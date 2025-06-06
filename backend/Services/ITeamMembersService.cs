using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface ITeamMembersService
{
    Task<IEnumerable<TeamMembers>> GetAllTeamMembersAsync();
    Task<TeamMembers?> GetTeamMemberByUserIdAsync(Guid userId);
    Task<TeamMembers> AddTeamMemberAsync(TeamMemberDto teamMember);
    Task<TeamMembers?> UpdateTeamMemberAsync(Guid userId, TeamMemberDto updatedMember);
    Task<bool> RemoveTeamMemberAsync(Guid userId);
    Task<bool> UpdateMembershipStatusAsync(Guid teamMemberId, int statusId);

}
