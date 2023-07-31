using System.ComponentModel;

namespace CashFlow.Domain.Enums
{
    public enum ETransactionType
    {
        [Description("Crédito")]
        Credit = 1,
        [Description("Débito")]
        Debit = 2
    }
}
