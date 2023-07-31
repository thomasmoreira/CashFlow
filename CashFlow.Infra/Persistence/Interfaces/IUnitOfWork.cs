using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Infra.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        bool HasActiveTransaction { get; }

        IDbContextTransaction GetCurrentTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitAsync(IDbContextTransaction transaction);
    }
}
