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

    public async Task<IEnumerable<Role>> GetAllAsync() => await _context.Roles.ToListAsync();

    public async Task<Role?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

    public async Task<bool> ExistsAsync(int id) => await _context.Roles.AnyAsync(r => r.Id == id);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();


    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}