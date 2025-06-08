using backend.Models;
namespace backend.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
    Task<Role?> GetByNameAsync(string roleName);
}
