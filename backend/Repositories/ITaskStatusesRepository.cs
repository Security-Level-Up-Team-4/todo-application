using backend.Models;

namespace backend.Repositories;

public interface ITaskStatusesRepository
{
    Task<IEnumerable<TaskStatuses>> GetAllAsync();
    Task<TaskStatuses?> GetByIdAsync(int id);
    Task<TaskStatuses> CreateAsync(TaskStatuses status);
    Task UpdateAsync(TaskStatuses status);
    Task DeleteAsync(int id);
}
