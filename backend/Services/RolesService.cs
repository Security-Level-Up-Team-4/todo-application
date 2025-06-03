using backend.Data;
using backend.DTOs;
using backend.Models;

namespace backend.Services;

public class RolesService : IRolesService
{
    private readonly TodoContext _context;

    public RolesService(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Roles>> GetAllRolesAsync()
    {
        return await Task.FromResult(_context.Roles.ToList());
    }

    public async Task<Roles?> GetRoleByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Roles> CreateRoleAsync(RolesDto roleDto)
    {
        var role = new Roles { Name = roleDto.Name };
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Roles?> UpdateRoleAsync(int id, RolesDto roleDto)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return null;

        role.Name = roleDto.Name;
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }
}