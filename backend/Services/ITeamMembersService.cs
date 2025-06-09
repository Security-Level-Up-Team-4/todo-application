using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface ITeamMembersService
{
    Task<IEnumerable<TeamMembers>> GetAllTeamMembersAsync();
    Task<TeamMembers?> GetTeamMembersByIdAsync(Guid teamId);
    Task<TeamMembers?> GetUserByTeamIdAsync(Guid teamId,Guid userId);
    Task<TeamMembers?> AddTeamMemberAsync(Guid teamId, string username, Guid requesterId);
    Task<TeamMembers?> RemoveTeamMemberAsync(Guid teamId, Guid userId);
    Task<TeamMembers?> UpdateMembershipStatusAsync(Guid teamId,Guid userId, int statusId);
    Task<List<TeamMemberDto>> GetUsersByTeamIdAsync(Guid teamId);


}
