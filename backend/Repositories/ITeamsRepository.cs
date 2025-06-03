using backend.Models;
namespace backend.Repositories;

public interface ITeamsRepository
{
    Task<IEnumerable<Teams>> GetAllAsync();
    Task<Teams> GetByIdAsync(Guid id);
    Task<Teams> AddAsync(Teams team);
    Task DeleteAsync(Guid id);
}
