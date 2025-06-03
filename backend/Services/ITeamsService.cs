using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface ITeamsService
{
    Task<IEnumerable<Teams>> GetAllTeamsAsync();
    Task<Teams?> GetTeamByIdAsync(Guid id);
    Task<Teams> CreateTeamAsync(TeamsDto team);
    Task<Teams?> UpdateTeamAsync(Guid id, TeamsDto updatedTeam);
    Task<bool> DeleteTeamAsync(Guid id);
}
