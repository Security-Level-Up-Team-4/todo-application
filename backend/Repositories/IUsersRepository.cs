using backend.Models;

namespace backend.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();
    Task<Users?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<Users?> GetByEmailAsync(string email);
     Task<Users?> GetByUserNameAsync(string email);
}
