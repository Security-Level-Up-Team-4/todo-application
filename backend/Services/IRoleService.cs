using backend.Models;
using backend.DTOs;
namespace backend.Services;

public interface IRolesService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleByIdAsync(int id);
    Task GetRoleByNameAsync(string name);
}
