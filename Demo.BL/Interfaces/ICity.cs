using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Interfaces
{
    public interface ICity
    {
        Task<IQueryable<City>> GetAsync(Expression<Func<City, bool>> filter);
    }
}
