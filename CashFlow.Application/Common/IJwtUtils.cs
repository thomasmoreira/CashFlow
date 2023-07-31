using CashFlow.Domain.Entities;

namespace CashFlow.Application.Common
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string token);
    }
}
