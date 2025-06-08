using backend.Data;
using backend.Repositories;
using backend.Models;

namespace backend.Services;

public class RolesService : IRolesService
{
    private readonly IRoleRepository _roleRepository;

    public RolesService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _roleRepository.GetByIdAsync(id);
    }

    public async Task GetRoleByNameAsync(string name)
    {
       var roleWithSameName = await _roleRepository.GetByNameAsync(name);
        if (roleWithSameName != null)
        {
            throw new InvalidOperationException($"Role '{name}' already exists.");
        }
    }

}