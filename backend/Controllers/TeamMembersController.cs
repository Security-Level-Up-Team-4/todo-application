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

    [HttpGet("{userId}")]
    public async Task<ActionResult<TeamMemberDto>> GetTeamMemberByUserId(Guid userId)
    {
        var member = await _teamMemberService.GetTeamMemberByUserIdAsync(userId);
        if (member == null)
            return NotFound();
        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<TeamMemberDto>> AddTeamMember(TeamMemberDto dto)
    {
        var newMember = await _teamMemberService.AddTeamMemberAsync(dto);
        return CreatedAtAction(nameof(GetTeamMemberByUserId), new { userId = newMember.UserId }, newMember);
    }

    [HttpPut("{teamMemberId}/status/{statusId:int}")]
    public async Task<IActionResult> UpdateMembershipStatus(Guid teamMemberId, int statusId)
    {
        var updated = await _teamMemberService.UpdateMembershipStatusAsync(teamMemberId, statusId);
        return updated ? NoContent() : NotFound();
    }

}
