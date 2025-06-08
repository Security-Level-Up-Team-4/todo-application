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

    public async Task<TaskStatuses> CreateAsync(string taskName)
    {
        var existingStatus = await _repository.GetByNameAsync(taskName);
        if (existingStatus != null)
        {
            throw new InvalidOperationException($"Task status '{taskName}' already exists.");
        }
        var status = new TaskStatuses { Name = taskName };
        var created = await _repository.CreateAsync(status);
        return created;
    }

    public async Task UpdateAsync(int id, string taskName)
    {
        var existingStatus = await _repository.GetByIdAsync(id);
        if (existingStatus == null)
        {
            throw new KeyNotFoundException($"Task status with ID {id} not found.");
        }


        var nameExists = await _repository.GetByNameAsync(taskName);
        if (nameExists != null && nameExists.Id != id)
        {
            throw new InvalidOperationException($"Task status '{taskName}' already exists.");
        }
    
        var status = new TaskStatuses { Name = taskName };
        await _repository.UpdateAsync(status);
    }
}
