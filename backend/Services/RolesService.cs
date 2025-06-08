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

    public async Task<IEnumerable<Roles>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<Roles?> GetRoleByIdAsync(int id)
    {
        return await _roleRepository.GetByIdAsync(id);
    }

    public async Task<Roles> CreateRoleAsync(string roleName)
    {
       await GetRoleByNameAsync(roleName);
        var newRole = new Roles
        {
            Name = roleName
        };

        return await _roleRepository.AddAsync(newRole);
    }
        

    public async Task<Roles?> UpdateRoleAsync(int id, string roleDto)
    {
        var existingRole = await _roleRepository.GetByIdAsync(id);

        if (existingRole == null)
        {
            throw new KeyNotFoundException($"Role with ID {id} not found.");
        }

        await GetRoleByNameAsync(roleDto);
        
        existingRole.Name = roleDto;
        return await _roleRepository.UpdateAsync(id, roleDto);
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