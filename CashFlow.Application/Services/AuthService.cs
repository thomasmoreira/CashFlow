using CashFlow.Api.Security;
using CashFlow.Application.Dtos;
using CashFlow.Application.Exceptions;
using CashFlow.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using static BCrypt.Net.BCrypt;

namespace CashFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private ILogger<AuthService> _logger;

        public AuthService(IUserService userService, ILogger<AuthService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<AuthenticateResponseDto> Authenticate(LoginRequest loginRequest)
        {
            _logger.LogInformation($"Recuperando informações do usuário {loginRequest.email}");

            var user = await _userService.GetByUsernameAsync(loginRequest.email);

            if (user == null || !Verify(loginRequest.password, user.PasswordHash))
                throw new BadRequestException("Usuário ou senha inválidos");

            _logger.LogInformation($"Gerando Token para o usuário {loginRequest.email}");

            var jwtToken = TokenService.GenerateToken(user);

            _logger.LogInformation($"Token gerado com sucesso para o usuário {loginRequest.email}");

            return new AuthenticateResponseDto(user, jwtToken);
        }
    }
}
