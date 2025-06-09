using backend.Data;
using backend.Models;
using backend.DTOs;
using backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services;

public class TeamMembersService : ITeamMembersService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMembershipStatusRepository _membershipStatusRepository;

    public TeamMembersService(ITeamMemberRepository teamMemberRepository, ITeamsRepository teamsRepository, IUsersRepository usersRepository, IMembershipStatusRepository membershipStatusRepository)
    {
        _teamMemberRepository = teamMemberRepository;
        _teamsRepository = teamsRepository;
        _usersRepository = usersRepository;
        _membershipStatusRepository = membershipStatusRepository; 
    }


    public async Task<IEnumerable<TeamMembers>> GetAllTeamMembersAsync()
    {
        return await _teamMemberRepository.GetAllAsync();
    }

    public async Task<TeamMembers?> GetTeamMembersByIdAsync(Guid teamId)
    {
        return await _teamMemberRepository.GetByIdAsync(teamId);
    }

    public async Task<TeamMembers?> AddTeamMemberAsync(Guid teamId, string username, Guid requesterId)
    {
        var team = await _teamsRepository.GetByIdAsync(teamId);
        if (team == null) throw new KeyNotFoundException("Team not found");
        if (team.CreatedBy != requesterId) throw new UnauthorizedAccessException("You are not a teamlead for this team");

        var user = await _usersRepository.GetByUserNameAsync(username);
        if (user == null) throw new KeyNotFoundException("No user with that username exists");

        var membershipStatus = await _membershipStatusRepository.GetByNameAsync("Active");
        var existingMember = await _teamMemberRepository.GetUserByTeamIdAsync(team.Id, user.Id);

        if (existingMember != null)
        {
            if (existingMember.MembershipStatusId == membershipStatus.Id)
            {
                throw new InvalidOperationException($"User {user.Id} is already a member of team {team.Id}.");
            }
            else
            {
                return await UpdateMembershipStatusAsync(team.Id, user.Id, membershipStatus.Id);
            }
        }
        else
        {
            var newMember = new TeamMembers
            {
                TeamId = team.Id,
                UserId = user.Id,
                MembershipStatusId = membershipStatus.Id,
                CreatedAt = DateTime.UtcNow,
            };
            return await _teamMemberRepository.AddAsync(newMember);
        }    
    }

    public async Task<TeamMembers?> GetUserByTeamIdAsync(Guid teamId, Guid userId)
    {
        var member = await _teamMemberRepository.GetUserByTeamIdAsync(teamId, userId);
        if (member == null)
        {
            throw new KeyNotFoundException($"User {userId} is not a member of team {teamId}.");
        }
        return member;
    }

    public async Task<TeamMembers?> RemoveTeamMemberAsync(Guid teamId, Guid userId)
    {
        var teamMember = await _teamMemberRepository.GetUserByTeamIdAsync(teamId, userId);
        if (teamMember == null)
        {
            throw new KeyNotFoundException($"User {userId} is not a member of team {teamId}.");
        }

        var membershipStatus = await _membershipStatusRepository.GetByNameAsync("Removed");

        return await UpdateMembershipStatusAsync(teamId, userId, membershipStatus.Id);
    }

    public async Task<TeamMembers?> UpdateMembershipStatusAsync(Guid teamId,Guid userId, int statusId)
    {
        var teamMember = await _teamMemberRepository.GetUserByTeamIdAsync(teamId, userId);
        if (teamMember == null) throw new KeyNotFoundException($"Team member with ID {teamId} not found.");
        
        teamMember.MembershipStatusId = statusId;
        if (teamMember.CreatedAt.Kind == DateTimeKind.Unspecified)
        {
            teamMember.CreatedAt = DateTime.SpecifyKind(teamMember.CreatedAt, DateTimeKind.Utc);
        }
        await _teamMemberRepository.UpdateAsync(teamMember);
        return teamMember;
    }
    
    public async Task<List<TeamMemberDto>> GetUsersByTeamIdAsync(Guid teamId)
    {
        var team = await _teamsRepository.GetByIdAsync(teamId);
        if (team == null) throw new KeyNotFoundException("Team not found");

        var teamMembers = await _teamMemberRepository.GetUsersByTeamIdAsync(teamId);
        return teamMembers.Select(tm => new TeamMemberDto
        {
            Id = tm.UserId,
            Username = _usersRepository.GetByIdAsync(tm.UserId).Result?.Username
        }).ToList();
        
    }

}
