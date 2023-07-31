using CashFlow.Application.Repositories;
using CashFlow.Domain.Entities;
using System.Data.Entity;

namespace CashFlow.Infra.Persistence.Repositories
{

    public class AccountRepository : IAccountRepository
    {
        private readonly CashFlowDbContext _context;

        public AccountRepository(CashFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            _context.Set<Account>().Add(account);

            await _context.SaveChangesAsync(cancellationToken);

            return account;

        }

        public async Task<IQueryable<Account>> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
                return _context.Set<Account>().AsNoTracking();
            else
                return _context.Set<Account>().AsQueryable();

        }

        public async Task<Account?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Account>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }
    }
}
