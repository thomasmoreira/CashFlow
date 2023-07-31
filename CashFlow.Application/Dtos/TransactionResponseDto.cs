using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using System.Globalization;

namespace CashFlow.Application.Dtos
{
    public class TransactionResponseDto
    {
        public int TransactionType { get; set; }
        public string TransactionTypeDesc { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public long AmountCents { get; set; }
        public string Amount { get; set; }
        public string? Description { get; set; }

        public static implicit operator TransactionResponseDto(Transaction transaction)
        {
            return new TransactionResponseDto
            {
                AccountId = transaction.AccountId,
                AccountName = "",
                AmountCents = transaction.AmountCents,
                Amount = ((decimal)transaction.AmountCents/100).ToString("C2", CultureInfo.CurrentCulture),
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate,
                TransactionType = (int)transaction.TransactionType,
                TransactionTypeDesc = transaction.TransactionType.GetDescription()
            };
        }

    }
}
