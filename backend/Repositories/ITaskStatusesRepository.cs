using backend.Models;

namespace backend.Repositories;

public interface ITaskStatusesRepository
{
    Task<IEnumerable<TaskStatuses>> GetAllAsync();
    Task<TaskStatuses?> GetByIdAsync(int id);
    Task<TaskStatuses?> GetByNameAsync(string name);
}
