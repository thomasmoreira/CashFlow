using CashFlow.Application.Dtos;
using CashFlow.Application.Extensions;
using CashFlow.Application.Repositories;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Entities;
using Serilog;
using Serilog.Context;

namespace CashFlow.Application.Services
{
    public class TransactionService : ITransactionService
    {        
        private readonly IBaseRepository<Transaction> _transactionRepository;

        private ILogger _logger = Log.ForContext<TransactionService>();

        public TransactionService(IBaseRepository<Transaction> transactionRepository)
        {            
            _transactionRepository = transactionRepository;
        }

        public async Task<Guid> AddTransaction(AddTransactionDto transactionDto)
        {
            LogContext.PushProperty("Transaction", transactionDto.ToJson());

            _logger.Information("Criando nova transação");

            Transaction transaction = (Transaction)transactionDto;

            var addedTransaction = await _transactionRepository.AddAsync(transaction);

            if (addedTransaction is null) 
            {
                _logger.Information("Nao foi possível criar a transação");
                return default;
            }

            _logger.Information("Transação criado com sucesso.");
            return addedTransaction.Id;
        }

        public async Task<TransactionResponseDto> GetTransaction(Guid id)
        {
            var result = await _transactionRepository.GetByIdAsync(id);
            TransactionResponseDto transaction = (TransactionResponseDto)result;

            return transaction;
        }

        public async Task<ICollection<TransactionResponseDto>> GetTransactions(GetTransactionsDto getTransactionsDto)
        {
            var result = await _transactionRepository.ListAsync(t => t.TransactionDate.Date == getTransactionsDto.TransactionDate.Date);
            return result.Select<Transaction, TransactionResponseDto>(t => t).ToList();

        }
    }
}
