using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class DistrictRep : IDistrict
    {

        private readonly ApplicationContext db;

        public DistrictRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IQueryable<District>> GetAsync(Expression<Func<District, bool>> filter)
        {
            var data = await db.District.Include(x => x.City).Where(filter).ToListAsync();
            return data.AsQueryable();
        }
    }
}
