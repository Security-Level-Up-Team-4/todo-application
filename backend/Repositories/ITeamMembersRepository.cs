using backend.Models;
namespace backend.Repositories;

public interface ITeamMemberRepository
{
    Task<IEnumerable<TeamMembers>> GetAllAsync();
    Task<TeamMembers> AddAsync(TeamMembers member);
    Task<TeamMembers?> GetByIdAsync(Guid userId);
    Task UpdateAsync(TeamMembers teamMember);
    Task<TeamMembers?> GetUserByTeamIdAsync(Guid teamId, Guid userId);
    Task<List<TeamMembers>> GetUsersByTeamIdAsync(Guid teamId);
}
