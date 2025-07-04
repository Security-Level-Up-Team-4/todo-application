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
    private readonly ITimelineEventRepository _timelineEventRepository;


    public TodosService(ITodosRepository repo, ITaskStatusesRepository taskStatusesRepository, IPrioritiesRepository prioritiesRepository,
        IUsersRepository usersRepository, ITeamsRepository teamsRepository, ITeamMemberRepository teamMembersRepository, IMembershipStatusRepository membershipStatusRepository, ITeamMembersService teamMembersService, ITimelineEventRepository timelineEventRepository)
    {
        _taskStatusesRepository = taskStatusesRepository;
        _prioritiesRepository = prioritiesRepository;
        _repo = repo;
        _usersRepository = usersRepository;
        _teamsRepository = teamsRepository;
        _teamMembersRepository = teamMembersRepository;
        _membershipStatusRepository = membershipStatusRepository;
        _teamMembersService = teamMembersService;
        _timelineEventRepository = timelineEventRepository;
    }

    public async Task<List<Todos>> GetAllAsync()
    {
        var todos = await _repo.GetAllAsync();
        return todos;
    }

    public async Task<TodosDTO?> GetByIdAsync(Guid todoId,Guid userId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null) throw new KeyNotFoundException($"Todo with ID {todoId} not found.");

        await isATeamMember(todo.TeamId,userId);

         return new TodosDTO
         {
             id = todo.id,
             title = todo.Title,
             description = todo.Description,
             priority = todo.PriorityId,
             priorityName = (await _prioritiesRepository.GetByIdAsync(todo.PriorityId)).Name,
             status = (await _taskStatusesRepository.GetByIdAsync(todo.StatusId)).name,
             createdBy = (await _usersRepository.GetByIdAsync(todo.CreatedBy)).Username,
             teamId = todo.TeamId,
             assignedTo = todo.assigned_to.HasValue ? (await _usersRepository.GetByIdAsync(todo.assigned_to.Value))?.Username ?? null : null,
             createdAt = todo.CreatedAt,
             updatedAt = todo.UpdatedAt,
             closedAt = todo.ClosedAt
         };
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
        if (team == null) throw new KeyNotFoundException("Team does not exist");

        var teamMembers = await _teamMembersService.GetUsersByTeamIdAsync(teamId);
        if (!teamMembers.Any(m => m.Id == userId))
            throw new KeyNotFoundException("User is not a member of this team.");

        var todoItems = await _repo.GetByTeamIdAsync(teamId);
        var teamCreator = await _usersRepository.GetByIdAsync(team.CreatedBy);

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
            Creator = teamCreator?.Username ?? "",
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
        var timelineEvent = new TimelineEvent
        {
            UserId = userId,
            TodoId = todo.id,
            EventType = "Todo creation",
            Description = $"Todo '{todo.Title}' created by {user.Username} in team {team.Name}.",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };
        await _timelineEventRepository.AddAsync(timelineEvent);
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

    public async Task<TodosDTO?> UpdateAssignedToAsync(Guid id, Guid assignedTo)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        if (todo == null)
            throw new KeyNotFoundException($"Todo not found.");
        var taskStatus = await _taskStatusesRepository.GetByNameAsync("Open");
        
        if (todo.StatusId != taskStatus.id)
            throw new InvalidOperationException("You may only assign open todo's");

        var user = await _usersRepository.GetByIdAsync(assignedTo);
        if (user == null)
            throw new KeyNotFoundException($"User not found.");
            
        await isATeamMember(todo.TeamId, assignedTo);

        todo.assigned_to = assignedTo;
        todo.StatusId = (await _taskStatusesRepository.GetByNameAsync("In Progress")).id;
        todo.CreatedAt = DateTime.SpecifyKind(todo.CreatedAt, DateTimeKind.Utc);
        todo.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        todo.ClosedAt = todo.ClosedAt.HasValue ? DateTime.SpecifyKind(todo.ClosedAt.Value, DateTimeKind.Utc) : null;

        await _repo.UpdateAsync(todo);
        var timelineEvent = new TimelineEvent
        {
            UserId = assignedTo,
            TodoId = todo.id,
            EventType = "Todo assignment",
            Description = $"Todo '{todo.Title}' is assigned to {user.Username}",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };
        await _timelineEventRepository.AddAsync(timelineEvent);

        return new TodosDTO
        {
            id = todo.id,
            title = todo.Title,
            description = todo.Description,
            priority = todo.PriorityId,
            priorityName = (await _prioritiesRepository.GetByIdAsync(todo.PriorityId)).Name,
            status = (await _taskStatusesRepository.GetByIdAsync(todo.StatusId)).name,
            createdBy = (await _usersRepository.GetByIdAsync(todo.CreatedBy)).Username,
            teamId = todo.TeamId,
            assignedTo = todo.assigned_to.HasValue ? (await _usersRepository.GetByIdAsync(todo.assigned_to.Value))?.Username ?? null : null,
            createdAt = todo.CreatedAt,
            updatedAt = todo.UpdatedAt,
            closedAt = todo.ClosedAt
        };
    }
    
    public async Task<TodosDTO?> UnassignAsync(Guid todoId, Guid userId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null)
            throw new KeyNotFoundException($"Todo not found.");
        var user = await _usersRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User not found.");

        if (todo.assigned_to != userId)
            throw new InvalidOperationException("Cannot unassign a todo that isn't assigned to you");
            
        await isATeamMember(todo.TeamId, userId);

        todo.assigned_to = null;
        todo.StatusId = (await _taskStatusesRepository.GetByNameAsync("Open")).id;
        todo.CreatedAt = DateTime.SpecifyKind(todo.CreatedAt, DateTimeKind.Utc);
        todo.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        todo.ClosedAt = todo.ClosedAt.HasValue ? DateTime.SpecifyKind(todo.ClosedAt.Value, DateTimeKind.Utc) : null;
        await _repo.UpdateAsync(todo);
        
        var timelineEvent = new TimelineEvent
        {
            UserId = userId,
            TodoId = todo.id,
            EventType = "Todo unassignment",
            Description = $"{user.Username} has be been unassigned from todo '{todo.Title}'",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };
        await _timelineEventRepository.AddAsync(timelineEvent);

        return new TodosDTO
        {
            id = todo.id,
            title = todo.Title,
            description = todo.Description,
            priority = todo.PriorityId,
            priorityName = (await _prioritiesRepository.GetByIdAsync(todo.PriorityId)).Name,
            status = (await _taskStatusesRepository.GetByIdAsync(todo.StatusId)).name,
            createdBy = (await _usersRepository.GetByIdAsync(todo.CreatedBy)).Username,
            teamId = todo.TeamId,
            assignedTo = todo.assigned_to.HasValue ? (await _usersRepository.GetByIdAsync(todo.assigned_to.Value))?.Username ?? null : null,
            createdAt = todo.CreatedAt,
            updatedAt = todo.UpdatedAt,
            closedAt = todo.ClosedAt
        };
    }

    public async Task<Todos?> UpdateUpdatedAtAsync(Guid id)
    {
        var todo = await CheckIfTodoExistsAsync(id);

        todo.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(todo);

        return todo;
    }

    public async Task<TodosDTO?> UpdateClosedAtAsync(Guid id, Guid userId)
    {
        var todo = await CheckIfTodoExistsAsync(id);
        if (todo == null) throw new KeyNotFoundException("Todo not found");

        var inProg = await _taskStatusesRepository.GetByNameAsync("In Progress");
        if (inProg.id != todo.StatusId) throw new InvalidOperationException("Todo is not in progress");

        if (userId != todo.assigned_to) throw new InvalidOperationException("Cannot close a todo assigned to someone else");

        var status = await _taskStatusesRepository.GetByNameAsync("Closed");
        var user = await _usersRepository.GetByIdAsync(userId);

        await isATeamMember(todo.TeamId, userId);


        todo.StatusId = status.id;
        todo.CreatedAt = DateTime.SpecifyKind(todo.CreatedAt, DateTimeKind.Utc);
        todo.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        todo.ClosedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        await _repo.UpdateAsync(todo);

        var timelineEvent = new TimelineEvent
        {
            UserId = userId,
            TodoId = todo.id,
            EventType = "Todo closure",
            Description = $"Todo '{todo.Title}' has been closed by {user.Username}.",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
        };
        await _timelineEventRepository.AddAsync(timelineEvent);

        return new TodosDTO
        {
            id = todo.id,
            title = todo.Title,
            description = todo.Description,
            priority = todo.PriorityId,
            priorityName = (await _prioritiesRepository.GetByIdAsync(todo.PriorityId)).Name,
            status = (await _taskStatusesRepository.GetByIdAsync(todo.StatusId)).name,
            createdBy = (await _usersRepository.GetByIdAsync(todo.CreatedBy)).Username,
            teamId = todo.TeamId,
            assignedTo = todo.assigned_to.HasValue ? (await _usersRepository.GetByIdAsync(todo.assigned_to.Value))?.Username ?? null : null,
            createdAt = todo.CreatedAt,
            updatedAt = todo.UpdatedAt,
            closedAt = todo.ClosedAt
        };
    }

    public async Task<TimelineEventDto> GetTimelineByTodoIdAsync(Guid todoId)
    {
        var todo = await _repo.GetByIdAsync(todoId);
        if (todo == null)
            throw new KeyNotFoundException($"Todo with ID {todoId} not found.");
        var timelineEvents = await _timelineEventRepository.GetByTodoIdAsync(todoId);
        var todoTimelineList = timelineEvents.Select(te => new TodoTimelineDto
        {
            Event = te.Description,
            CreatedAt = DateTime.SpecifyKind(te.CreatedAt, DateTimeKind.Utc)
        }).ToList();
        var timelineEventDtos =new TimelineEventDto
        {
            Id = todo.id,
            Title = todo.Title,
            Timeline = todoTimelineList,
        };
        return timelineEventDtos;
    }
}
