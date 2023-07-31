using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Persistence.Interfaces
{
    public interface ICashFlowDbContext : IUnitOfWork
    {
        DbSet<RolePrivilege> RolePrivileges { get; }
        DbSet<Role> Roles { get; }
        DbSet<User> Users { get; }
        DbSet<Transaction> Transactions { get; }
        DbSet<Account> Accounts { get; }
    }
}
