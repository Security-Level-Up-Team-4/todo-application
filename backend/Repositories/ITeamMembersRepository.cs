using backend.Models;
namespace backend.Repositories;

public interface ITeamMemberRepository
{
    Task<IEnumerable<TeamMembers>> GetAllAsync();
    Task<TeamMembers> AddAsync(TeamMembers member);
}
