using backend.Models;

namespace backend.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();
    Task<Users?> GetByIdAsync(Guid id);
    Task AddAsync(Users user);
    Task UpdateAsync(Users user);
    Task DeleteAsync(Guid id);
    Task<Users?> GetByEmailAsync(string email);
}
