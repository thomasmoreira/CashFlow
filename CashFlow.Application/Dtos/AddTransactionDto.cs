using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;

namespace CashFlow.Application.Dtos
{
    public class AddTransactionDto
    {
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountType { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public static implicit operator Transaction(AddTransactionDto transaction) 
        {
            return new Transaction(transaction.TransactionDate, (EAccountType)transaction.AccountType, (decimal)transaction.Amount, transaction.Description, (ETransactionType)transaction.TransactionType);
        }
    }
}
