using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface ITodosService
{
    Task<List<Todos>> GetAllAsync();
    Task<Todos?> GetByIdAsync(Guid id);
    Task AddAsync(TodosDTO dto);
    Task UpdateAsync(TodosDTO dto);
    Task DeleteAsync(Guid id);
}
