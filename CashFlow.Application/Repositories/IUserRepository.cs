using CashFlow.Domain.Entities;
using System.Linq.Expressions;

namespace CashFlow.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User transaction, CancellationToken cancellationToken = default);
        Task<User?> GetBySpecAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);        
        Task<IList<User>> ListAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
