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


    public async Task<User?> GetBySpecAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(predicate);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IQueryable<User>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (asNoTracking)
            return _context.Set<User>().AsNoTracking();
        else
            return _context.Set<User>().AsQueryable(); ;
    }
}
