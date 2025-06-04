using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class TaskStatusesService : ITaskStatusesService
{
    private readonly ITaskStatusesRepository _repository;

    public TaskStatusesService(ITaskStatusesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskStatuses>> GetAllAsync()
    {
        var statuses = await _repository.GetAllAsync();
        return statuses;
    }

    public async Task<TaskStatuses?> GetByIdAsync(int id)
    {
        var status = await _repository.GetByIdAsync(id);
        return status;
    }

    public async Task<TaskStatuses> CreateAsync(TaskStatusesDTO dto)
    {
        var status = new TaskStatuses { Name = dto.Name };
        var created = await _repository.CreateAsync(status);
        return created;
    }

    public async Task UpdateAsync(TaskStatusesDTO dto)
    {
        var status = new TaskStatuses { Name = dto.Name };
        await _repository.UpdateAsync(status);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
