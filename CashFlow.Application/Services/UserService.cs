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

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var result = await _repository.GetBySpecAsync(u => u.Username == username);
            return result;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        public async Task<IQueryable<User>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync();
        }
      
    }
}
