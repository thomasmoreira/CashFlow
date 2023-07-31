using CashFlow.Application.Dtos;
using CashFlow.Application.Repositories;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Entities;


namespace CashFlow.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid?> AddAccount(AddAccountDto accountDto)
        {
            Account account = (Account)accountDto;
            var addedAccount = await _repository.AddAsync(account);

            return addedAccount is not null ? addedAccount.Id : default;
        }

        public async Task<ICollection<AccountResponseDto>> GetAccounts() 
        {
            var result = await _repository.GetAll();
            return result.Select<Account, AccountResponseDto>(a => a).ToList();
        }

        public async Task<AccountResponseDto?> GetAccount(Guid id)
        {
            var result = await _repository.GetById(id);
            return result is null ? default : (AccountResponseDto)result;
        }
    }
}
