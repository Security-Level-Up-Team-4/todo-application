using backend.Models;
using backend.DTOs;
namespace backend.Services;

public interface IRolesService
{
    Task<IEnumerable<Roles>> GetAllRolesAsync();
    Task<Roles?> GetRoleByIdAsync(int id);
    Task GetRoleByNameAsync(string name);
}
