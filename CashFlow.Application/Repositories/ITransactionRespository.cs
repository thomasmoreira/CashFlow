using CashFlow.Application.Dtos;
using CashFlow.Domain.Entities;
using System.Linq.Expressions;

namespace CashFlow.Application.Repositories
{
    public interface ITransactionRespository
    {
        Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task<Transaction?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<IQueryable<Transaction>> GetAll(bool asNoTracking = true);
        Task<IList<Transaction>> ListAsync(Expression<Func<Transaction, bool>> predicate, CancellationToken cancellationToken = default);


    }
}
