using Microsoft.EntityFrameworkCore;
using Pinewood.Customers.Core.Interfaces;
using System.Linq.Expressions;

namespace Pinewood.Customers.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CustomerDbContext dbContext;

        protected GenericRepository(CustomerDbContext context)
        {
            dbContext = context;
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public async Task<bool > ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbContext.Set<T>().AnyAsync(predicate);
        }
    }
}
