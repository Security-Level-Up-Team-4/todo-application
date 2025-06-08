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

    public async Task<Priorities> CreateAsync(Priorities priority)
    {
        _context.Priorities.Add(priority);
        await _context.SaveChangesAsync();
        return priority;
    }

    public async Task<Priorities?> UpdateAsync(int id, string updatedPriority)
    {
        var existing = await _context.Priorities.FindAsync(id);
        if (existing == null) 
        {
            throw new KeyNotFoundException($"Priority with ID {id} not found.");
        }

        var existingPriority = await GetByNameAsync(updatedPriority);
        
        if (existingPriority != null)
        {
            throw new InvalidOperationException($"Priority '{updatedPriority}' already exists.");
        }

        existing.Name = updatedPriority;
        _context.Priorities.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Priorities?> GetByNameAsync(string name)
    {
        return await _context.Priorities.FirstOrDefaultAsync(p => p.Name == name);
    }
}
