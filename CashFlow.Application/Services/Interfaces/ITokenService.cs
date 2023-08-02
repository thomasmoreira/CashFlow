using CashFlow.Domain.Entities;

namespace CashFlow.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string>GenerateToken(User user);
    }
}
