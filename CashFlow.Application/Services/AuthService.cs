using CashFlow.Application.Common;
using CashFlow.Application.Dtos;
using CashFlow.Application.Exceptions;
using CashFlow.Application.Services.Interfaces;

namespace CashFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private IJwtUtils _jwtUtils;

        public AuthService(IUserService userService, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        public async Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userService.GetByEmailAsync(loginRequest.email);

            // validate
            if (user == null /*|| !BCrypt.Verify(loginRequest.password, user.PasswordHash)*/)
                throw new BadRequestException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
