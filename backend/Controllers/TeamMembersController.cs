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
        await CheckIDs(teamId, userId);

        var member = await _teamMemberService.GetUserByTeamIdAsync(teamId, userId);
        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpPost("{teamId}/users/{userId}")]
    public async Task<ActionResult<TeamMemberDto>> AddTeamMember(Guid teamId, Guid userId)
    {
        await CheckIDs(teamId, userId);

        var newMember = await _teamMemberService.AddTeamMemberAsync(teamId, userId);
        return CreatedAtAction(nameof(GetTeamMemberByUserId), new { userId = newMember.UserId }, newMember);
    }

    [HttpDelete("{teamId}/users/{userId}")]
    public async Task<IActionResult> RemoveTeamMember(Guid teamId, Guid userId)
    {
       await CheckIDs(teamId, userId);

        var removedMember = await _teamMemberService.RemoveTeamMemberAsync(teamId, userId);
        if (removedMember == null)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{teamMemberId}/users/{userId}/status/{statusId:int}")]
    public async Task<IActionResult> UpdateMembershipStatus(Guid teamMemberId, Guid userId, int statusId)
    {
        await CheckIDs(teamMemberId, userId);

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

    public async Task CheckIDs(Guid teamId, Guid userId)
    {
        if (teamId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("Team ID and User ID must be provided.");
        }
    }
}
