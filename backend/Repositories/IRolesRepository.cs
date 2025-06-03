using backend.Models;
namespace backend.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Roles>> GetAllAsync();
    Task<Roles?> GetByIdAsync(int id);
    Task<Roles> AddAsync(Roles role);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}
