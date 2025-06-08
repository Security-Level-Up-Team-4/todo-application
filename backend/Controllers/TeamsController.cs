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
    private readonly IUserContextService _userContextService;

    public TeamsController(ITeamsService teamService, IUserContextService userContextService)
    {
        _teamService = teamService;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamsDto>> GetTeamById(Guid id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
            return NotFound();
        return Ok(team);
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
            return CreatedAtAction(nameof(GetTeamById), new { id = newTeam.Id }, newTeam);
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
    [HttpPut("name/{name}")]
    public async Task<ActionResult<TeamsDto>> UpdateTeam(string name)
    {
        
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Team name cannot be empty.");

        try
        {
            var id = _userContextService.GetUserId();
            var updatedTeam = await _teamService.UpdateTeamAsync(id, name);
            return Ok(updatedTeam);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
