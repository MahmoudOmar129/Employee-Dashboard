using Demo.BL.Interfaces;
using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace Demo.BL.Repository
{
    public class GenericRep<TEntity> : IGenericRep<TEntity> where TEntity : class
    {


        private readonly ApplicationContext Context;
        private DbSet<TEntity> dbSet;

        public GenericRep(ApplicationContext Context)
        {
            this.Context = Context;
            this.dbSet = Context.Set<TEntity>();
        }

        public async Task<int> CountAsync()
        {
            var data = await dbSet.CountAsync();
            return data;
        }
    }
}
