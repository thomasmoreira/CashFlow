using CashFlow.Application.Dtos;
using CashFlow.Application.Repositories;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Entities;
using Serilog;
using Serilog.Context;

namespace CashFlow.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRespository _repository;
        private ILogger _logger = Log.ForContext<TransactionService>();

        public TransactionService(ITransactionRespository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddTransaction(AddTransactionDto transactionDto)
        {
            LogContext.PushProperty("Transaction", transactionDto);

            _logger.Information("Criando nova transação");

            Transaction transaction = (Transaction)transactionDto;

            var addedTransaction = await _repository.AddAsync(transaction);

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
            var result = await _repository.GetById(id);
            TransactionResponseDto transaction = (TransactionResponseDto)result;

            return transaction;
        }

        public async Task<ICollection<TransactionResponseDto>> GetTransactions()
        {
            var result = await _repository.GetAll();
            return result.Select<Transaction, TransactionResponseDto>(t => t).ToList();

        }
        public async Task<ICollection<TransactionResponseDto>> GetTransactions(DateTime date)
        {
            var result = await _repository.ListAsync(t => t.TransactionDate.Date == date.Date);
            return result.Select<Transaction, TransactionResponseDto>(t => t).ToList();

        }
    }
}
