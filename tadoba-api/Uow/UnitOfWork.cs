using Microsoft.EntityFrameworkCore;
using tadoba_api.Entity;

namespace tadoba_api.Uow
{
    /// <summary>
    /// This is implementation of IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region private properties
        private readonly TadobaDbContext _dbContext;

        private bool disposed = false;
        #endregion

        #region public properties
        public TadobaDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }
        #endregion

        #region constructor
        public UnitOfWork(TadobaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        #endregion

        #region public method

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void RollbackAsync()
        {
            _dbContext.DisposeAsync();
        }
        #endregion
    }
}