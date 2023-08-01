using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Persistence.Interfaces
{
    public interface ICashFlowDbContext : IUnitOfWork
    {
        DbSet<User> Users { get; }
        DbSet<Transaction> Transactions { get; }
    }
}
