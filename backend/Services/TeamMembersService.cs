using backend.Data;
using backend.Models;
using backend.DTOs;

using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TeamMembersService : ITeamMembersService
{
    private readonly TodoContext _context;

    public TeamMembersService(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamMembers>> GetAllTeamMembersAsync()
    {
        return await _context.TeamMembers.ToListAsync();
    }

    public async Task<TeamMembers?> GetTeamMemberByUserIdAsync(Guid userId)
    {
        return await _context.TeamMembers.FirstOrDefaultAsync(m => m.UserId == userId);
    }

    public async Task<TeamMembers> AddTeamMemberAsync(TeamMemberDto teamMember)
    {
        var newMember = new TeamMembers
        {
            UserId = teamMember.UserId,
            TeamId = teamMember.TeamId,
            MembershipStatusId = teamMember.MembershipStatusId
        };
        _context.TeamMembers.Add(newMember);
        await _context.SaveChangesAsync();
        return newMember;
    }

    public async Task<TeamMembers?> UpdateTeamMemberAsync(Guid userId, TeamMemberDto updatedMember)
    {
        var existing = await _context.TeamMembers.FirstOrDefaultAsync(m => m.UserId == userId);
        if (existing == null) return null;

        existing.TeamId = updatedMember.TeamId;
        existing.MembershipStatusId = updatedMember.MembershipStatusId;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> RemoveTeamMemberAsync(Guid userId)
    {
        var member = await _context.TeamMembers.FirstOrDefaultAsync(m => m.UserId == userId);
        if (member == null) return false;

        _context.TeamMembers.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }
}
