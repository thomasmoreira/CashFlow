using CashFlow.Domain.Entities;
using CashFlow.Infra.Persistence.Configuration;
using CashFlow.Infra.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

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
        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<RolePrivilege> RolePrivileges => Set<RolePrivilege>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<User> Users => Set<User>();

        #endregion


        private IDbContextTransaction _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;



        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //builder.ApplyConfiguration(new TransactionConfiguration());
            //builder.ApplyConfiguration(new AccountConfiguration());

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
