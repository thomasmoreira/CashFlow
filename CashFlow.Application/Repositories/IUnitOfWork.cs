using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Application.Repositories
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
