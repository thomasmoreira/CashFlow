using CashFlow.Application.Common;
using CashFlow.Application.Dtos;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest);
    }
}
