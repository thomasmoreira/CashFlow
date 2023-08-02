using CashFlow.Application.Dtos;

namespace CashFlow.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> AddTransaction(AddTransactionDto transaction);
        Task<ICollection<TransactionResponseDto>> GetTransactions(GetTransactionsDto date);
        Task<TransactionResponseDto> GetTransaction(Guid id);

    }
}
