using backend.Models;

namespace backend.Repositories;

public interface ITodosRepository
{
    Task<List<Todos>> GetAllAsync();
    Task<Todos?> GetByIdAsync(Guid id);
    Task AddAsync(Todos todo);
    Task UpdateAsync(Todos todo);
    Task DeleteAsync(Guid id);
}
