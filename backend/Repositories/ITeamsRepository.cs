using backend.Models;
namespace backend.Repositories;

public interface ITeamsRepository
{
    Task<IEnumerable<Teams>> GetAllAsync();
    Task<IEnumerable<Teams>> GetAllByUserIdAsync(Guid userId);
    Task<Teams> GetByIdAsync(Guid id);
    Task<Teams?> GetByTeamNameAsync(string teamName);
    Task<Teams?> UpdateTeamAsync(Teams team);
    Task<Teams> AddAsync(Teams team);
    Task DeleteAsync(Guid id);

}
