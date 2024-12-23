using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Interfaces
{
    public interface IDistrict
    {
        Task<IQueryable<District>> GetAsync(Expression<Func<District, bool>> filter);
    }
}
