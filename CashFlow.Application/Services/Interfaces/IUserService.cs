using CashFlow.Domain.Entities;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
