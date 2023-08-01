using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;

namespace CashFlow.Application.Dtos
{
    public class UserReponseDto
    {
        public string Username { get; set; }
        public string Role { get; set; }

        public static implicit operator UserReponseDto(User user) 
        {
            return new UserReponseDto { Username = user.Username, Role = user.Role.GetDescription() };
        }
    }
}
