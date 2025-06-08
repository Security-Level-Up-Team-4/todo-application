using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly TodoContext _context;
    public TeamMemberRepository(TodoContext context) => _context = context;

    public async Task<IEnumerable<TeamMembers>> GetAllAsync() =>
        await _context.TeamMembers.ToListAsync();

    public async Task<TeamMembers> AddAsync(TeamMembers member)
    {
        _context.TeamMembers.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<TeamMembers?> GetByIdAsync(Guid userId)
    {
        return await _context.TeamMembers
            .FirstOrDefaultAsync(tm => tm.UserId == userId);
    }

    public async Task UpdateAsync(TeamMembers teamMember)
    {
        _context.TeamMembers.Update(teamMember);
        await _context.SaveChangesAsync();
    }

    public async Task<TeamMembers?> GetUserByTeamIdAsync(Guid teamId, Guid userId)
    {
        return await _context.TeamMembers
            .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);
    }

    public async Task<List<TeamMembers>> GetUsersByTeamIdAsync(Guid teamId)
    {
        return await _context.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .ToListAsync();
    }
}
