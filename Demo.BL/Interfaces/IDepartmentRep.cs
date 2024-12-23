using Demo.DAL.Entity;

namespace Demo.BL.Interfaces
{

    /// <summary>
    /// This Interface For Department Business
    /// </summary>
    public interface IDepartmentRep
    {

        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <returns>Task IQueryable Department</returns>
        Task<IQueryable<Department>> GetAsync();
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department obj);
        Task UpdateAsync(Department obj);
        Task DeleteAsync(Department obj);
    }
}
