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
    private readonly ITeamMemberRepository _teamMembersRepository;
    private readonly IMembershipStatusRepository _membershipStatusRepository;
     private readonly ITeamMembersService _teamMembersService;


    public TodosService(ITodosRepository repo, ITaskStatusesRepository taskStatusesRepository, IPrioritiesRepository prioritiesRepository,
        IUsersRepository usersRepository, ITeamsRepository teamsRepository, ITeamMemberRepository teamMembersRepository, IMembershipStatusRepository membershipStatusRepository, ITeamMembersService teamMembersService)
    {
        _taskStatusesRepository = taskStatusesRepository;
        _prioritiesRepository = prioritiesRepository;
        _repo = repo;
        _usersRepository = usersRepository;
        _teamsRepository = teamsRepository;
        _teamMembersRepository = teamMembersRepository;
        _membershipStatusRepository = membershipStatusRepository;
        _teamMembersService = teamMembersService;
    }

    public async Task<List<Todos>> GetAllAsync()
    {
        var todos = await _repo.GetAllAsync();
        return todos;
    }

    public async Task<Todos?> GetByIdAsync(Guid todoId,Guid userId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null) throw new KeyNotFoundException($"Todo with ID {todoId} not found.");

        await isATeamMember(todo.TeamId,userId);

        return todo;
    }
    public async Task isATeamMember(Guid teamId, Guid userId)
    {
        var teamMember = await  _teamMembersRepository.GetUserByTeamIdAsync(teamId, userId);
        var membershipStatus = await _membershipStatusRepository.GetByNameAsync("Removed");
        if (teamMember == null || teamMember.MembershipStatusId == membershipStatus.Id) throw new KeyNotFoundException($"User is not a member of the team's todo.");//TODO a better exception
    }

public async Task<TeamDetailsDto?> GetByTeamIdAsync(Guid teamId, Guid userId)
{
        var team = await _teamsRepository.GetByIdAsync(teamId);
        var teamMembers = await _teamMembersService.GetUsersByTeamIdAsync(teamId);
        var todoItems = await _repo.GetByTeamIdAsync(teamId);

        var taskStatuses = await _taskStatusesRepository.GetAllAsync();
        var statusMap = taskStatuses.ToDictionary(s => s.id, s => s.name);

        if (todoItems == null) throw new KeyNotFoundException($"No todos found for team with ID {teamId}.");
       var  usersList = teamMembers.Select(member => new UserDto
        {
            Id = member.Id,
            Username = member.Username,
        }).ToList();
        var TodosList = todoItems.Select(todo => new TodosDTO
        {
            id = todo.id,
            title = todo.Title,
            description = todo.Description,
            priority = todo.PriorityId,
            status = statusMap.TryGetValue(todo.StatusId, out var statusName) ? statusName : "Unknown"

        }).ToList();
        return new TeamDetailsDto
        {
            TeamId = team.Id,
            TeamName = team.Name,
            Users = usersList,
            Todos = TodosList
        };  
}

    public async Task<TodosDTO> AddAsync(TodosDTO dto, Guid userId, Guid teamId)
    {
        var taskStatus = await _taskStatusesRepository.GetByNameAsync("Open");
        var priority = await _prioritiesRepository.GetByIdAsync(dto.priority);
        var user = await _usersRepository.GetByIdAsync(userId);
        if (user == null) throw new KeyNotFoundException($"User with ID {userId} not found.");
        var team = await _teamsRepository.GetByIdAsync(teamId);
        if (team == null) throw new KeyNotFoundException($"Team with ID {teamId} not found.");
        await isATeamMember(team.Id, userId);
        var todo = new Todos
        {
            id = Guid.NewGuid(),
            Title = dto.title,
            Description = dto.description,
            PriorityId = dto.priority,
            StatusId = taskStatus.id,
            CreatedBy = userId,
            TeamId = team.Id,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(todo);
        return new TodosDTO
        {
            id = todo.id,
            title = todo.Title,
            description = todo.Description,
            priority = todo.PriorityId,
            priorityName = priority.Name,
            status = taskStatus.name
        };
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
        var taskStatus = await _taskStatusesRepository.GetByNameAsync("Active");
        
        if (todo.StatusId == taskStatus.id)
            throw new InvalidOperationException("Cannot assign a todo that is not active.");

        var user = _usersRepository.GetByIdAsync(assignedTo);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {assignedTo} not found.");
            
        await isATeamMember(todo.TeamId, assignedTo);

        todo.assigned_to = assignedTo;
        await _repo.UpdateAsync(todo);

        return todo;
    }
    
    public async Task<Todos?> UnassignAsync(Guid todoId, Guid userId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null)
            throw new KeyNotFoundException($"Todo with ID {todoId} not found.");
        var user = await _usersRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");
            
        await isATeamMember(todo.TeamId, userId);

        todo.assigned_to = null;
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

        todo.StatusId = status.id;
        todo.ClosedAt = DateTime.UtcNow;
        todo.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(todo);

        return todo;
    }
}
