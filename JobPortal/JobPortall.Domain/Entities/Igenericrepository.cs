using JobPortal.Domain.Entities;
using JobPortal.JobPortall.Domain.Entities;
using System.Linq.Expressions;

namespace JobPortal.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id); // Soft Delete
        Task<bool> ExistsAsync(int id);
    }
}
