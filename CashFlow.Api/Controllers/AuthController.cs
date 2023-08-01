using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequestDto loginRequest)
        {
            _logger.LogInformation($"Usuário {loginRequest.username} solicitando autenticação");

            var result = await _authService.Authenticate(loginRequest);

            if (result is null)
                _logger.LogInformation($"Tentativa de autenticação inválida para o usuário {loginRequest.username}");

            _logger.LogInformation($"Autenticação realizada com sucesso para ao usuário {loginRequest.username}");

            return Ok(result);
        }
    }
}
