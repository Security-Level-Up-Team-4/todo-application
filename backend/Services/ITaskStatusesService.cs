using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface ITaskStatusesService
{
    Task<IEnumerable<TaskStatuses>> GetAllAsync();
    Task<TaskStatuses?> GetByIdAsync(int id);
    Task<TaskStatuses> CreateAsync(string dto);
    Task UpdateAsync(int id, string dto);
}
