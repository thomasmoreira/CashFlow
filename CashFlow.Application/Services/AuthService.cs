using CashFlow.Api.Security;
using CashFlow.Application.Common;
using CashFlow.Application.Dtos;
using CashFlow.Application.Exceptions;
using CashFlow.Application.Services.Interfaces;
using static BCrypt.Net.BCrypt;

namespace CashFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userService.GetByUsernameAsync(loginRequest.email);

            if (user == null || !Verify(loginRequest.password, user.PasswordHash))
                throw new BadRequestException("Usuário ou senha inválidos");

            var jwtToken = TokenService.GenerateToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
