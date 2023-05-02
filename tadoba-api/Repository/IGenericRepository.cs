using System.Linq.Expressions;

namespace tadoba_api.Repository
{
    //Here, we are creating the IGenericRepository interface as a Generic Interface
    //Here, we are applying the Generic Constraint 
    //The constraint is, T is going to be a class
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id, string includeProperties = "");
        Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int first = 0, int offset = 0);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, string includeProperties = "");
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
