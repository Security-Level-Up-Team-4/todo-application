using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class TodosService : ITodosService
{
    private readonly ITodosRepository _repo;
    private readonly ITaskStatusesRepository _taskStatusesRepository;
    private readonly IPrioritiesRepository _prioritiesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ITeamsRepository _teamsRepository;


    public TodosService(ITodosRepository repo, ITaskStatusesRepository taskStatusesRepository, IPrioritiesRepository prioritiesRepository,
        IUsersRepository usersRepository, ITeamsRepository teamsRepository)
    {
        _taskStatusesRepository = taskStatusesRepository;
        _prioritiesRepository = prioritiesRepository;
        _repo = repo;
        _usersRepository = usersRepository;
        _teamsRepository = teamsRepository;
    }

    public async Task<List<Todos>> GetAllAsync()
    {
        var todos = await _repo.GetAllAsync();
        return todos;
    }

    public async Task<Todos?> GetByIdAsync(Guid todoId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null) throw new KeyNotFoundException($"Todo with ID {todoId} not found.");

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

    public async Task<Todos> CheckIfTodoExistsAsync(Guid id)
    {
        var todo = await _repo.GetByIdAsync(id);
        if (todo == null) throw new KeyNotFoundException($"Todo with ID {id} not found.");
        return todo;
    }

    public async Task<Todos?> UpdateTitleAsync(Guid id, string title)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var existingTodo = await _repo.GetByNameAsync(id, title);//TOASK: can a todo have the same title in the same team?
        if (existingTodo != null)
            throw new InvalidOperationException($"A todo with the title '{title}' already exists in the team.");

        todo.Title = title;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateDescriptionAsync(Guid id, string description)
    {
        var todo = await CheckIfTodoExistsAsync(id);

        todo.Description = description;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdatePriorityAsync(Guid id, int priorityId)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var priority = _prioritiesRepository.GetByIdAsync(priorityId);
        if (priority == null)
            throw new KeyNotFoundException($"Priority with ID {priorityId} not found.");

        todo.PriorityId = priorityId;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateStatusAsync(Guid id, int statusId)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var status = _taskStatusesRepository.GetByIdAsync(statusId);
        if (status == null)
            throw new KeyNotFoundException($"Status with ID {statusId} not found.");

        todo.StatusId = statusId;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateTeamAsync(Guid id, Guid teamId)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var team = await _teamsRepository.GetByIdAsync(teamId);
        if (team == null)
            throw new KeyNotFoundException($"Team with ID {teamId} not found.");

        todo.TeamId = teamId;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateAssignedToAsync(Guid id, Guid assignedTo)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var user = _usersRepository.GetByIdAsync(assignedTo);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {assignedTo} not found.");

        todo.AssignedTo = assignedTo;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateUpdatedAtAsync(Guid id)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        
        todo.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<Todos?> UpdateClosedAtAsync(Guid id)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        var status = await _taskStatusesRepository.GetByNameAsync("Closed");

        todo.StatusId = status.Id;
        todo.ClosedAt = DateTime.UtcNow;
        todo.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(todo);

        return todo;
    }
}
