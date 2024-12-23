using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Demo.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {



        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<Department>> GetAsync()
        {
            var data = await db.Department.Select(a => a).ToListAsync();
            return data.AsQueryable();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Department.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateAsync(Department obj)
        {
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }


        public async Task UpdateAsync(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();

        }


    }
}
