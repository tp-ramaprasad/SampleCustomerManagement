using System.Linq.Expressions;

namespace Pinewood.Customers.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
