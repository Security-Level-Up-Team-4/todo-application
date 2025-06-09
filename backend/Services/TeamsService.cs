using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TeamsService : ITeamsService
{
    private readonly ITeamsRepository _teamsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ITeamMembersService _teamMembersService;
    private readonly ITodosRepository _todosRepository;
    private readonly IPrioritiesRepository _prioritiesRepository;


    public TeamsService(ITeamsRepository teamsRepository, IUsersRepository usersRepository, ITeamMembersService teamMembersService, ITodosRepository todosRepository, IPrioritiesRepository prioritiesRepository)
    {
        _teamsRepository = teamsRepository;
        _usersRepository = usersRepository;
        _teamMembersService = teamMembersService;
        _todosRepository = todosRepository;
        _prioritiesRepository = prioritiesRepository;
    }

    public async Task<IEnumerable<Teams>> GetAllTeamsAsync()
    {
        return await _teamsRepository.GetAllAsync();
    }

    public async Task<TeamDetailsDto?> GetTeamByIdAsync(Guid id)
    {
        var team = await _teamsRepository.GetByIdAsync(id);
        var teamMembers = await _teamMembersService.GetUsersByTeamIdAsync(id);
        var todoItems = await _todosRepository.GetByTeamIdAsync(id);
        return new TeamDetailsDto
        {
            TeamId = team.Id,
            TeamName = team.Name,
            Todos = todoItems.Select(todo => new TodosDTO
            {
                title = todo.Title,
                description = todo.Description,
                priority = todo.PriorityId,
                priorityName = _prioritiesRepository.GetByIdAsync(todo.PriorityId).Result?.Name,
            }).ToList(),
            Users = teamMembers.Select(member => new UserDto
            {
                Id = member.Id,
                Username = member.Username,
            }).ToList()
        };  
    }

    public async Task<IEnumerable<Teams>> GetAllTeamsByUserIdAsync(Guid userId)
    {
        var userIdExists = await _usersRepository.GetByIdAsync(userId);
        if (userIdExists == null) throw new KeyNotFoundException($"User with ID {userId} not found.");
        
        return await _teamsRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<Teams> CreateTeamAsync(string name, Guid createdBy)
    {
        var existingTeam = await _teamsRepository.GetByTeamNameAsync(name);
        if (existingTeam != null)
        {
            throw new InvalidOperationException($"A team with the name '{name}' already exists.");
        }

        var userExists = await _usersRepository.GetByIdAsync(createdBy);
        if (userExists == null)
        {
            throw new KeyNotFoundException($"User with ID {createdBy} not found.");
        }

        var team = new Teams
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        var newTeam = await _teamsRepository.AddAsync(team);
        await _teamMembersService.AddTeamMemberAsync(newTeam.Id ,userExists.Username);

        return newTeam;
    }

    public async Task<Teams?> UpdateTeamAsync(Guid id, string name)
    {
        var existingTeam = await _teamsRepository.GetByIdAsync(id);
        if (existingTeam == null)
        {
            throw new KeyNotFoundException($"Team with ID {id} not found.");
        }

        var teamWithSameName = await _teamsRepository.GetByTeamNameAsync(name);
        if (teamWithSameName != null)
        {
            throw new InvalidOperationException($"A team with the name '{name}' already exists.");
        }

        existingTeam.Name = name;
        return await _teamsRepository.UpdateTeamAsync(existingTeam);
    }
    
}
