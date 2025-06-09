using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface ITeamsService
{
    Task<IEnumerable<Teams>> GetAllTeamsAsync();
    Task<IEnumerable<Teams>> GetAllTeamsByUserIdAsync(Guid userId);
    Task<TeamDetailsDto?> GetTeamByIdAsync(Guid id);
    Task<Teams> CreateTeamAsync(string name, Guid createdBy);
    Task<Teams?> UpdateTeamAsync(Guid id, string name);
}
