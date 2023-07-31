using CashFlow.Application.Repositories;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CashFlow.Infra.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRespository
    {
        private readonly CashFlowDbContext _context;

        public TransactionRepository(CashFlowDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            _context.Set<Transaction>().Add(transaction);

            await _context.SaveChangesAsync(cancellationToken);

            return transaction;
        }

        public async Task<IQueryable<Transaction>> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
                return _context.Set<Transaction>().AsNoTracking();
            else
                return _context.Set<Transaction>().AsQueryable();
        }

        public async Task<Transaction?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Transaction>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public async Task<IList<Transaction>> ListAsync(Expression<Func<Transaction, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Transaction>().Where(predicate).ToListAsync(cancellationToken);
        }

    }
}
