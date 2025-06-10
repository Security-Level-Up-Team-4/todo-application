using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamService;
    private readonly ITodosService _todoService;
    private readonly IUserContextService _userContextService;

    public TeamsController(ITeamsService teamService, IUserContextService userContextService, ITodosService todosService)
    {
        _teamService = teamService;
        _todoService = todosService;
        _userContextService = userContextService;
    }

    [HttpGet]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult<IEnumerable<TeamsDto>>> GetAllTeamsByUserId()
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var teams = await _teamService.GetAllTeamsByUserIdAsync(userId);
            return Ok(teams);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("{teamId:guid}")]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult<TeamDetailsDto>> GetByTeamId(Guid teamId)
    {
        try
        {
            var userId = _userContextService.GetUserId();

            var teamDetails = await _todoService.GetByTeamIdAsync(teamId, userId);
            if (teamDetails == null)
                return NotFound(new { message = "404: Team not found" });

            return Ok(teamDetails);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "team_lead")]
    public async Task<ActionResult<TeamsDto>> CreateTeam([FromBody] CreateTeamDto createTeamDto)
    {
        if (string.IsNullOrWhiteSpace(createTeamDto.Name))
            return BadRequest(new { message = "Team name cannot be empty." });

        if (createTeamDto.Name.Length > 100)
            return BadRequest(new { message = "Team name cannot be over 100 characters." });

        try
        {
            var createdBy = _userContextService.GetUserId();
            var newTeam = await _teamService.CreateTeamAsync(createTeamDto.Name, createdBy);
            return CreatedAtAction(nameof(CreateTeam), new { id = newTeam.Id }, newTeam);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
