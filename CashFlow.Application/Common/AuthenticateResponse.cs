using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Extensions;

namespace CashFlow.Application.Common
{
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            UserId = user.Id;
            Username = user.Username;
            Role = user.Role.GetDescription();
            Token = token;
        }
    }
}
