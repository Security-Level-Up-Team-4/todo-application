using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UsersService(IUsersRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<UsersDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var role = await _roleRepository.GetAllAsync();
        return users.Select(user => new UsersDTO
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            RoleId = user.RoleId,
            RoleName = role.FirstOrDefault(r => r.Id == user.RoleId).Name
        });
    }

    public async Task<UsersDTO?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        var role = await _roleRepository.GetByIdAsync(user.RoleId);

        return user == null ? null : new UsersDTO
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            RoleId = user.RoleId,
            RoleName = role.Name
        };
    }

    public async Task<UsersDTO?> updateUserRoleAsync(Guid userId, string roleName)
    {
        var user =await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        var role =await _roleRepository.GetByNameAsync(roleName);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role '{roleName}' not found.");
        }

        user.RoleId = role.Id;
        _userRepository.UpdateAsync(user);

        return new UsersDTO
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            RoleId = role.Id,
            RoleName = role.Name
        };
    }
}
