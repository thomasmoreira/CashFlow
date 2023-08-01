using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Repositories
{
    public interface ICashFlowDbContext : IUnitOfWork
    {
        DbSet<User> Users { get; }
        DbSet<Transaction> Transactions { get; }
    }
}
