using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Data.Base
    {
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
        {
        Task<IEnumerable<T>> GetallAsync();
        Task<IEnumerable<T>> GetallAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        }
    }
