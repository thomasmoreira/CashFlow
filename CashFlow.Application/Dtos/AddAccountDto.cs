using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;

namespace CashFlow.Application.Dtos
{
    public class AddAccountDto
    {
        public string Name { get; set; }
        public int AccountType { get; set; }

        public static implicit operator Account(AddAccountDto account) 
        {
            return new Account(account.Name, (EAccountType)account.AccountType);
        }

    }
}
