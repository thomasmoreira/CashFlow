using CashFlow.Application.Repositories;
using CashFlow.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CashFlowDbContext _context;

    public UserRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Set<User>().Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> GetBySpecAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == "teste");
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public Task<IList<User>> ListAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
