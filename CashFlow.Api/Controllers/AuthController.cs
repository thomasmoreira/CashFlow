using CashFlow.Application.Common;
using CashFlow.Application.Dtos;
using CashFlow.Application.Exceptions;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authService.Authenticate(loginRequest);
            return Ok();
        }

        private string GenerateAccessToken(Guid userId, Guid roleId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.Secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtConst.Issuer,
                Audience = JwtConst.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtConst.UserId, userId.ToString()),
                new Claim(JwtConst.RoleId, roleId.ToString())
            }),
                Expires = DateTime.Now.AddMinutes(JwtConst.ExpiryMinutes),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
