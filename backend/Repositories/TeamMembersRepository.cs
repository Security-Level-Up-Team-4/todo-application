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
}
