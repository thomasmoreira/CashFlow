using CashFlow.Domain.Entities;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string email);
    }
}
