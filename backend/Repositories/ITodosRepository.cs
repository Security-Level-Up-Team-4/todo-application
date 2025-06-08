using backend.Models;

namespace backend.Repositories;

public interface ITodosRepository
{
    Task<List<Todos>> GetAllAsync();
    Task<Todos?> GetByIdAsync(Guid id);
    Task AddAsync(Todos todo);
    Task UpdateAsync(Todos todo);
    Task<Todos?> GetByNameAsync(Guid teamId, string name);
    Task<List<Todos>> GetByTeamIdAsync(Guid teamId);
}
