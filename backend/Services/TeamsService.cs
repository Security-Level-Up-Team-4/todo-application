using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TeamsService : ITeamsService
{
    private readonly TodoContext _context;

    public TeamsService(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Teams>> GetAllTeamsAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<Teams?> GetTeamByIdAsync(Guid id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<Teams> CreateTeamAsync(TeamsDto team)
    {
        var newTeam = new Teams
        {
            Id = Guid.NewGuid(),
            Name = team.Name,
            CreatedBy = team.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
        _context.Teams.Add(newTeam);
        await _context.SaveChangesAsync();
        return newTeam;
    }

    public async Task<Teams?> UpdateTeamAsync(Guid id, TeamsDto updatedTeam)
    {
        var existing = await _context.Teams.FindAsync(id);
        if (existing == null) return null;

        existing.Name = updatedTeam.Name;
        existing.CreatedBy = updatedTeam.CreatedBy;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteTeamAsync(Guid id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return false;

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        return true;
    }
}
