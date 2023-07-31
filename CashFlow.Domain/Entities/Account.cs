using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class Account
    {
        public Account(string name, EAccountType accountType)
        {
            Id = Guid.NewGuid();
            Name = name;
            AccountType = accountType;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public EAccountType AccountType { get; private set; }
    }
}
