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

}
