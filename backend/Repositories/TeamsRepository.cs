using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
namespace backend.Repositories;

public class TeamRepository : ITeamsRepository
{
    private readonly AppDbContext _context;
    public TeamRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Teams>> GetAllAsync() =>
        await _context.Teams.ToListAsync();

    public async Task<Teams> GetByIdAsync(Guid id) =>
        await _context.Teams.FindAsync(id);

    public async Task<Teams> AddAsync(Teams team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task DeleteAsync(Guid id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team != null)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Teams>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Teams
            .Where(t => t.CreatedBy == userId)
            .ToListAsync();
    }
    public async Task<Teams?> GetByTeamNameAsync(string teamName)
    {
        return await _context.Teams
            .FirstOrDefaultAsync(t => t.Name == teamName);
    }

    public async Task<Teams?> UpdateTeamAsync(Teams team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
        return team;
    }
   
}
