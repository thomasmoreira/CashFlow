using CashFlow.Application.Dtos;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);
        Task<IQueryable<Account>> GetAll(bool asNoTracking = true);
        Task<Account?> GetById(Guid id, CancellationToken cancellationToken = default);

    }
}
