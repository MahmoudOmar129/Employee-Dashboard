using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class CountryRep : ICountry
    {

        private readonly ApplicationContext db;

        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<Country>> GetAsync(Expression<Func<Country, bool>> filter)
        {
            var data = await db.Country.Where(filter).ToListAsync();
            return data.AsQueryable();

        }


    }
}
