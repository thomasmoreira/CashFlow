using CashFlow.Domain.Entities;
using System.Linq.Expressions;

namespace CashFlow.Application.Repositories
{
    public interface IUserRepository
    {        
        Task<User?> GetBySpecAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);        
        Task<IQueryable<User>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}
