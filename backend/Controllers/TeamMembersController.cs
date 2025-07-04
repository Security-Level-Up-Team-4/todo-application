using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMembersService _teamMemberService;
    private readonly IUserContextService _userContextService;

    public TeamMembersController(ITeamMembersService teamMemberService, IUserContextService userContextService)
    {
        _teamMemberService = teamMemberService;
        _userContextService = userContextService;
    }

    [HttpPost]
    [Authorize(Roles = "team_lead")]
    public async Task<ActionResult<TeamMemberDto>> AddTeamMember([FromBody] AddTeamMemberRequest request)
    {
        try
        {
            var userId = _userContextService.GetUserId();

            if (string.IsNullOrWhiteSpace(request.Username))
                return BadRequest(new { message = "Username cannot be empty." });
            if (request.TeamId == Guid.Empty)
                return BadRequest(new { message = "Team id required." });

            var newMember = await _teamMemberService.AddTeamMemberAsync(request.TeamId, request.Username, userId);
            return Created($"/api/teammembers/", newMember);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("users")]
    [Authorize(Roles = "team_lead")]
    public async Task<IActionResult> RemoveTeamMembers([FromBody] RemoveTeamMembersDto dto)
    {
        try
        {
            var callerUserId = _userContextService.GetUserId();

            if (dto.TeamId == Guid.Empty)
                return BadRequest(new { message = "Team ID required" });
            if (dto.UserIds == null || !dto.UserIds.Any())
                return BadRequest(new { message = "At least one user ID must be provided." });

            foreach (var userId in dto.UserIds)
            {
                await _teamMemberService.RemoveTeamMemberAsync(dto.TeamId, userId, callerUserId);
            }

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
