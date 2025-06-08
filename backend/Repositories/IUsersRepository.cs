using backend.Models;

namespace backend.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
     Task<User?> GetByUserNameAsync(string email);
}
