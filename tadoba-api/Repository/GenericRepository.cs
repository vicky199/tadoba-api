using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using tadoba_api.Entity;
using tadoba_api.Uow;

namespace tadoba_api.Repository
{//The following GenericRepository class Implement the IGenericRepository Interface
    //And Here T is going to be a class
    //While Creating an Instance of the GenericRepository type, we need to specify the Class Name
    //That is we need to specify the actual class name of the type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region private fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region constructor
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region public Methods

        /// <summary>
        ///  This method will fetch data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(long id, string includeProperties = "")
        {
            var model = await _unitOfWork.DbContext.Set<T>().FindAsync(id);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _unitOfWork.DbContext.Entry(model).Reference(includeProperty).Load();
            }

            return model;
        }

        /// <summary>
        /// this method will fetch first data by given filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.DbContext.Set<T>();
            query = query.Where(filter);
            foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// this method will fetch list of data by given filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="first"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int first = 0, int offset = 0)
        {
            IQueryable<T> query = _unitOfWork.DbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (offset > 0)
            {
                query = query.Skip(offset);
            }
            if (first > 0)
            {
                query = query.Take(first);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// this method will add data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _unitOfWork.DbContext.Set<T>().AddAsync(entity)
                .ConfigureAwait(true);

            return entity;
        }

        /// <summary>
        /// this method will add  list of data
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _unitOfWork.DbContext.Set<T>().AddRangeAsync(entities)
                .ConfigureAwait(true);
            return entities;
        }

        /// <summary>
        /// this method will update data
        /// </summary>
        /// <param name="entity"></param>
        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                _unitOfWork.DbContext.Entry(entity).State = EntityState.Modified;
            });
        }

        /// <summary>
        /// this method will update data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _unitOfWork.DbContext.Set<T>().Remove(entity);
            await _unitOfWork.DbContext.SaveChangesAsync()
                .ConfigureAwait(true);
        }

        /// <summary>
        /// this method will update list of data
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _unitOfWork.DbContext.Set<T>().RemoveRange(entities);
            await _unitOfWork.DbContext.SaveChangesAsync()
                .ConfigureAwait(true);
        }
        #endregion
    }
}