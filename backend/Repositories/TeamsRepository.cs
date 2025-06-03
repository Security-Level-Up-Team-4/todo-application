using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
namespace backend.Repositories;
public class TeamRepository : ITeamsRepository
{
    private readonly TodoContext _context;
    public TeamRepository(TodoContext context) => _context = context;

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
}
