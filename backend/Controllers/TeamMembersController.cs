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

    [HttpPost]
    public async Task<ActionResult<TeamMemberDto>> AddTeamMember([FromBody] AddTeamMemberRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || request.TeamId == Guid.Empty)
            return BadRequest("Team id or username cannot be empty.");

        var newMember = await _teamMemberService.AddTeamMemberAsync(request.TeamId, request.Username);
        return Created($"/api/teammembers/", newMember);
    }

    [HttpPut("users")]
    public async Task<IActionResult> RemoveTeamMembers([FromBody] RemoveTeamMembersDto dto)
    {
        if (dto.TeamId == Guid.Empty || dto.UserIds == null || !dto.UserIds.Any())
        {
            return BadRequest("Team ID and at least one user ID must be provided.");
        }

        foreach (var userId in dto.UserIds)
        {
            await _teamMemberService.RemoveTeamMemberAsync(dto.TeamId, userId);
        }

        return NoContent();
    }

    [HttpGet("{teamId}/users")]
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
