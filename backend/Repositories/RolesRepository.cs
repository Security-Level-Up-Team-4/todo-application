using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly TodoContext _context;

    public RoleRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Roles>> GetAllAsync() => await _context.Roles.ToListAsync();

    public async Task<Roles?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

    public async Task<Roles> AddAsync(Roles role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> ExistsAsync(int id) => await _context.Roles.AnyAsync(r => r.Id == id);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}