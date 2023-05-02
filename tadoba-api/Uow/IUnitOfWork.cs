using tadoba_api.Entity;

namespace tadoba_api.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        TadobaDbContext DbContext { get; }
        Task SaveAsync();
        void RollbackAsync();
    }
}
