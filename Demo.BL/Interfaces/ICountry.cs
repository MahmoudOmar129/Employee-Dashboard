using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Interfaces
{
    public interface ICountry
    {
        Task<IQueryable<Country>> GetAsync(Expression<Func<Country, bool>> filter);
    }
}
