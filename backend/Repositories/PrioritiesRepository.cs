using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PrioritiesRepository : IPrioritiesRepository
{
    private readonly AppDbContext _context;

    public PrioritiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Priorities>> GetAllAsync()
    {
        return await _context.Priorities.ToListAsync();
    }

    public async Task<Priorities?> GetByIdAsync(int id)
    {
        return await _context.Priorities.FindAsync(id);
    }

    public async Task<Priorities?> GetByNameAsync(string name)
    {
        return await _context.Priorities.FirstOrDefaultAsync(p => p.Name == name);
    }
}
