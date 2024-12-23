using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Interfaces
{
    public interface IEmployeeRep
    {

        // GetByIdAsync( x => x.IsActive == True && x.IsDeleted == false && x.Id == id )
        Task<IQueryable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter);
        Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter);
        Task CreateAsync(Employee obj);
        Task UpdateAsync(Employee obj);
        Task DeleteAsync(Employee obj);
        Task ReActivate(int? id);

    }
}
