using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipStatusController : ControllerBase
{
    private readonly IMembershipStatusService _membershipStatusService;

    public MembershipStatusController(IMembershipStatusService membershipStatusService)
    {
        _membershipStatusService = membershipStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipStatusDto>>> GetAllStatuses()
    {
        var statuses = await _membershipStatusService.GetAllStatusesAsync();
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MembershipStatusDto>> GetStatusById(int id)
    {
        var status = await _membershipStatusService.GetStatusByIdAsync(id);
        if (status == null)
            return NotFound();
        return Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<MembershipStatusDto>> CreateStatus([FromBody] string statusName)
    {
        var newStatus = await _membershipStatusService.CreateStatusAsync(statusName);
        return CreatedAtAction(nameof(GetStatusById), new { id = newStatus.Id }, newStatus);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MembershipStatusDto>> UpdateStatus(int id, [FromBody] string updatedStatus)
    {
        var existingStatus = await _membershipStatusService.UpdateStatusAsync(id, updatedStatus);
        if (existingStatus == null)
            return NotFound();
        return Ok(existingStatus);
    }
}
