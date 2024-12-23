using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class CityRep : ICity
    {

        private readonly ApplicationContext db;

        public CityRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<City>> GetAsync(Expression<Func<City, bool>> filter)
        {
            var data = await db.City.Include(x => x.Country).Where(filter).ToListAsync();
            return data.AsQueryable();
        }
    }
}
