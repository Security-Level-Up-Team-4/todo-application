using backend.Models;
using backend.DTOs;
namespace backend.Services;

public interface IRolesService
{
    Task<IEnumerable<Roles>> GetAllRolesAsync();
    Task<Roles?> GetRoleByIdAsync(int id);
    Task<Roles> CreateRoleAsync(string roleDto);
    Task<Roles?> UpdateRoleAsync(int id, string roleDto);
    Task GetRoleByNameAsync(string name);
}
