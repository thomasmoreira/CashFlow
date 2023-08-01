using CashFlow.Domain.Entities;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string email);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IQueryable<User>> GetAllAsync(bool asNotracking = true, CancellationToken cancellationToken = default);
    }
}
