using CashFlow.Application.Repositories;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Services
{    
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _repository.GetBySpecAsync(u => u.Email == email);
            return result;
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = _repository.GetByIdAsync(id);
            return result;
        }
    }
}
