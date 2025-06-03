using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PrioritiesRepository : IPrioritiesRepository
{
    private readonly TodoContext _context;

    public PrioritiesRepository(TodoContext context)
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

    public async Task<Priorities> CreateAsync(Priorities priority)
    {
        _context.Priorities.Add(priority);
        await _context.SaveChangesAsync();
        return priority;
    }
}
