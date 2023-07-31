using System.ComponentModel;

namespace CashFlow.Domain.Enums
{
    public enum EAccountType
    {
        [Description("Outros")]
        Unknown = 1,
        [Description("Conta Corrente")]
        CurrentAccount = 2,
        [Description("Conta Poupança")]
        SavingsAccount = 3
    }
}
