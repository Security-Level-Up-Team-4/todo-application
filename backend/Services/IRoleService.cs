using backend.Models;
using backend.DTOs;
namespace backend.Services;

public interface IRolesService
{
    Task<IEnumerable<Roles>> GetAllRolesAsync();
    Task<Roles?> GetRoleByIdAsync(int id);
    Task<Roles> CreateRoleAsync(RolesDto roleDto);
    Task<Roles?> UpdateRoleAsync(int id, RolesDto roleDto);
    Task<bool> DeleteRoleAsync(int id);
}
