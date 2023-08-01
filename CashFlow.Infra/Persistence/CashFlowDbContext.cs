using CashFlow.Application.Repositories;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Infra.Persistence
{
    public class CashFlowDbContext : DbContext, ICashFlowDbContext
    {
        public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options) 
        {
            this.Database.EnsureCreated();            
        }

        #region DbSets

        public DbSet<Transaction> Transactions => Set<Transaction>();        
        public DbSet<User> Users => Set<User>();

        #endregion


        private IDbContextTransaction _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;



        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null!;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        private void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }
    }
}
