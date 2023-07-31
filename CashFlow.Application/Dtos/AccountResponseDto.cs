using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;

namespace CashFlow.Application.Dtos
{
    public class AccountResponseDto
    {        
        public string Name { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeDesc { get; set; }

        public static implicit operator AccountResponseDto(Account account)
        {
            return new AccountResponseDto { Name = account.Name, AccountType = (int)account.AccountType, AccountTypeDesc = account.AccountType.GetDescription() };
        }
    }
}
