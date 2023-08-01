using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class Transaction
    {
        protected Transaction() { }
        public Transaction(DateTime transactionDate, EAccountType accountType, decimal amount, string description, ETransactionType transactionType)
        {
            Id = Guid.NewGuid();
            TransactionDate = transactionDate;
            AccountType = accountType;
            AmountCents = TransactionType == ETransactionType.Debit ? Convert.ToInt64((amount*100))*-1 : Convert.ToInt64((amount * 100));
            Description = description;
            TransactionType = transactionType;
        }

        public Guid Id { get; private set; }
        public ETransactionType TransactionType { get; set; }
        public EAccountType AccountType { get; set; }
        public DateTime TransactionDate { get; private set; }        
        public long AmountCents { get; private set; }
        public string Description { get; private set; }
        
    }
}
