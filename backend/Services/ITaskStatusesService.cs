using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface ITaskStatusesService
{
    Task<IEnumerable<TaskStatuses>> GetAllAsync();
    Task<TaskStatuses?> GetByIdAsync(int id);
    Task<TaskStatuses> CreateAsync(TaskStatusesDTO dto);
    Task UpdateAsync(TaskStatusesDTO dto);
    Task DeleteAsync(int id);
}
