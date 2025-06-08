using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMembersService _teamMemberService;

    public TeamMembersController(ITeamMembersService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetAllTeamMembers()
    {
        var members = await _teamMemberService.GetAllTeamMembersAsync();
        return Ok(members);
    }

    [HttpGet("{teamId}")]
    public async Task<ActionResult<TeamMemberDto>> GetTeamMemberByUserId(Guid teamId)
    {
        var member = await _teamMemberService.GetTeamMembersByIdAsync(teamId);
        if (member == null)
            return NotFound();
        return Ok(member);
    }

    [HttpGet("{teamId}/users/{userId}")]
    public async Task<ActionResult<TeamMemberDto>> GetTeamMemberByUserId(Guid teamId, Guid userId)
    {
        if (teamId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("Team ID and User ID must be provided.");
        }

        var member = await _teamMemberService.GetUserByTeamIdAsync(teamId, userId);
        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<TeamMemberDto>> AddTeamMember([FromBody] AddTeamMemberRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.TeamName))
            return BadRequest("Team name or username cannot be empty.");

        var newMember = await _teamMemberService.AddTeamMemberAsync(request.TeamName, request.Username);
        return CreatedAtAction(nameof(GetTeamMemberByUserId), new { userId = newMember.UserId }, newMember);
    }

    [HttpDelete("users")]
    public async Task<IActionResult> RemoveTeamMember(Guid teamId, Guid userId)
    {
       if (teamId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("Team ID and User ID must be provided.");
        }

        var removedMember = await _teamMemberService.RemoveTeamMemberAsync(teamId, userId);
        if (removedMember == null)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{teamMemberId}/users/{userId}/status/{statusId:int}")]
    public async Task<IActionResult> UpdateMembershipStatus(Guid teamMemberId, Guid userId, int statusId)
    {
        if (teamMemberId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("Team ID and User ID must be provided.");
        }

        var updated = await _teamMemberService.UpdateMembershipStatusAsync(teamMemberId, userId, statusId);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpGet("teams/{teamId}/users")]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetUsersByTeamId(Guid teamId)
    {
        if (teamId == Guid.Empty)
        {
            return BadRequest("Team ID must be provided.");
        }

        var users = await _teamMemberService.GetUsersByTeamIdAsync(teamId);
        return Ok(users);
    }
}
