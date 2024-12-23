using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRep : IEmployeeRep
    {

        private readonly ApplicationContext db;

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee.Include(dpt => dpt.Department)
                                        .Include(dis => dis.District)
                                        .ThenInclude(c => c.City)
                                        .ThenInclude(cntry => cntry.Country)
                                        .Where(filter).ToListAsync();
            return data.AsQueryable();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee.Include(dpt => dpt.Department)
                                        .Include(dis => dis.District)
                                        .ThenInclude(c => c.City)
                                        .ThenInclude(cntry => cntry.Country)
                                        .Where(filter).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateAsync(Employee obj)
        {
            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee obj)
        {
            obj.IsUpdated = true;
            obj.UpdatedOn = DateTime.Now;

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee obj)
        {

            var oldData = db.Employee.Find(obj.Id);

            oldData.IsDeleted = true;
            oldData.IsActive = false;

            oldData.DeletedOn = DateTime.Now;

            await db.SaveChangesAsync();
        }

        public async Task ReActivate(int? id)
        {

            var oldData = db.Employee.Find(id);

            oldData.IsDeleted = false;
            oldData.IsActive = true;
            oldData.IsUpdated = true;
            oldData.UpdatedOn = DateTime.Now;

            await db.SaveChangesAsync();
        }
    }
}
