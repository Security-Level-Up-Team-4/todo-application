using backend.Models;
namespace backend.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Roles>> GetAllAsync();
    Task<Roles?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
    Task<Roles?> GetByNameAsync(string roleName);
}
