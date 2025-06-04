using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class TodosService : ITodosService
{
    private readonly ITodosRepository _repo;

    public TodosService(ITodosRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Todos>> GetAllAsync()
    {
        var todos = await _repo.GetAllAsync();
        return todos;
    }

    public async Task<Todos?> GetByIdAsync(Guid id)
    {
        var todo = await _repo.GetByIdAsync(id);
        return todo;
    }

    public async Task AddAsync(TodosDTO dto)
    {
        var todo = new Todos
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            PriorityId = dto.PriorityId,
            StatusId = dto.StatusId,
            CreatedBy = dto.CreatedBy,
            TeamId = dto.TeamId,
            AssignedTo = dto.AssignedTo,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(todo);
    }

    public async Task UpdateAsync(TodosDTO dto)
    {
        var todo = new Todos
        {
            Title = dto.Title,
            Description = dto.Description,
            PriorityId = dto.PriorityId,
            StatusId = dto.StatusId,
            CreatedBy = dto.CreatedBy,
            TeamId = dto.TeamId,
            AssignedTo = dto.AssignedTo,
            UpdatedAt = DateTime.UtcNow,
            ClosedAt = dto.ClosedAt
        };

        await _repo.UpdateAsync(todo);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
