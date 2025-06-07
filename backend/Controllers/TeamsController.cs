using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamService;

    public TeamsController(ITeamsService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamsDto>>> GetAllTeams()
    {
        var teams = await _teamService.GetAllTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamsDto>> GetTeamById(Guid id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
            return NotFound();
        return Ok(team);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TeamsDto>>> GetAllTeamsByUserId(Guid userId)
    {
        try
        {
            var teams = await _teamService.GetAllTeamsByUserIdAsync(userId);
            return Ok(teams);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{name}/user/{createdBy}")]
    public async Task<ActionResult<TeamsDto>> CreateTeam(string name, Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Team name cannot be empty.");

        try
        {
            var newTeam = await _teamService.CreateTeamAsync(name, createdBy);
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
    [HttpPut("{id}/name/{name}")]
    public async Task<ActionResult<TeamsDto>> UpdateTeam(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Team name cannot be empty.");

        try
        {
            var updatedTeam = await _teamService.UpdateTeamAsync(id, name);
            return Ok(updatedTeam);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
