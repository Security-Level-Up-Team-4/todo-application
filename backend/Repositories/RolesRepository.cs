using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Roles>> GetAllAsync() => await _context.Role.ToListAsync();

    public async Task<Roles?> GetByIdAsync(int id) => await _context.Role.FindAsync(id);

    public async Task<bool> ExistsAsync(int id) => await _context.Roles.AnyAsync(r => r.Id == id);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();


    public async Task<Roles?> GetByNameAsync(string roleName)
    {
        return await _context.Role.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}