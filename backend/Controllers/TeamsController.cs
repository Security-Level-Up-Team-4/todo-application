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
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{teamId:guid}")]
    public async Task<ActionResult<TeamDetailsDto>> GetByTeamId(Guid teamId)
    {
        var userId = _userContextService.GetUserId();

        var teamDetails = await _todoService.GetByTeamIdAsync(teamId, userId);
        if (teamDetails == null)
            return NotFound();

        return Ok(teamDetails);
    }

    [HttpPost]
    public async Task<ActionResult<TeamsDto>> CreateTeam([FromBody] CreateTeamDto createTeamDto)
    {
        if (string.IsNullOrWhiteSpace(createTeamDto.Name))
            return BadRequest("Team name cannot be empty.");

        try
        {
            var createdBy = _userContextService.GetUserId();
            var newTeam = await _teamService.CreateTeamAsync(createTeamDto.Name, createdBy);
            return CreatedAtAction(nameof(CreateTeam), new { id = newTeam.Id }, newTeam);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
