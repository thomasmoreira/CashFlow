using CashFlow.Application.Dtos;

namespace CashFlow.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Guid?> AddAccount(AddAccountDto account);
        Task<AccountResponseDto> GetAccount(Guid id);
        Task<ICollection<AccountResponseDto>> GetAccounts();
    }
}
