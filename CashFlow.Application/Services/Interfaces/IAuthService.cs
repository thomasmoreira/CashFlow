using CashFlow.Application.Dtos;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponseDto> Authenticate(LoginRequest loginRequest);
    }
}
