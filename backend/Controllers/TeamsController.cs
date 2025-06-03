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

    [HttpPost]
    public async Task<ActionResult<TeamsDto>> CreateTeam(TeamsDto createDto)
    {
        var newTeam = await _teamService.CreateTeamAsync(createDto);
        return CreatedAtAction(nameof(GetTeamById), new { id = newTeam.Id }, newTeam);
    }
}
