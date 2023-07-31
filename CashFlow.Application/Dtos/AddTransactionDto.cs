using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;

namespace CashFlow.Application.Dtos
{
    public class AddTransactionDto
    {
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public static implicit operator Transaction(AddTransactionDto transaction) 
        {
            return new Transaction(transaction.TransactionDate, transaction.AccountId, (decimal)transaction.Amount, transaction.Description, (ETransactionType)transaction.TransactionType);
        }
    }
}
