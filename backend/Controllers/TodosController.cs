using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodosService _service;
    private readonly IUserContextService _userContextService;

    public TodosController(ITodosService service, IUserContextService userContextService)
    {
        _service = service;
        _userContextService = userContextService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var userId = _userContextService.GetUserId();
        var todo = await _service.GetByIdAsync(id,userId);
        return Ok(todo);
    }

    [HttpGet("teams/{teamId:guid}")]
    public async Task<ActionResult<TeamDetailsDto>> GetByTeamId(Guid teamId)
    {
        var userId = _userContextService.GetUserId();

        var teamDetails = await _service.GetByTeamIdAsync(teamId, userId);
        if (teamDetails == null)
            return NotFound();

        return Ok(teamDetails);
    }

    [HttpPost("{teamId:guid}")]
    public async Task<ActionResult> Create([FromBody] TodosDTO dto, Guid teamId)
    {

        var userId = _userContextService.GetUserId();

        var newTodo = await _service.AddAsync(dto, userId, teamId);
        return CreatedAtAction(nameof(Create), new { id = newTodo.id }, newTodo);
    }

    [HttpPut("{id:guid}/title")]
    public async Task<ActionResult> UpdateTitle(Guid id, [FromBody] string title)
    {
        var updated = await _service.UpdateTitleAsync(id, title);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/description")]
    public async Task<ActionResult> UpdateDescription(Guid id, [FromBody] string description)
    {
        var updated = await _service.UpdateDescriptionAsync(id, description);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/priority/{priorityId:int}")]
    public async Task<ActionResult> UpdatePriority(Guid id, int priorityId)
    {
        var updated = await _service.UpdatePriorityAsync(id, priorityId);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/status/{statusId:int}")]
    public async Task<ActionResult> UpdateStatus(Guid id, int statusId)
    {
        var updated = await _service.UpdateStatusAsync(id, statusId);
        return Ok(updated);
    }

    // [HttpPut("{id:guid}/team/{teamId:guid}")]
    // public async Task<ActionResult> UpdateTeam(Guid id, Guid teamId)
    // {
    //     var updated = await _service.UpdateTeamAsync(id, teamId);
    //     return Ok(updated);
    // }

    /*[HttpPut("{id:guid}/assign/{userId:guid}")]
    public async Task<ActionResult> UpdateAssignedTo(Guid id, Guid userId)
    {
        var updated = await _service.UpdateAssignedToAsync(id, userId);
        return Ok(updated);
    }*/

     [HttpPut("assign/{todoId:guid}")]
    public async Task<ActionResult> UpdateAssignedTo(Guid todoId)
    {
        var userId = _userContextService.GetUserId();
        var updated = await _service.UpdateAssignedToAsync(todoId, userId);
        return Ok(updated);
    }

    [HttpPut("unassign/{todoId:guid}")]
    public async Task<ActionResult> Unassign(Guid todoId)
    {
        var userId = _userContextService.GetUserId();
        var updated = await _service.UnassignAsync(todoId, userId);
        return Ok(updated);
    }

    // [HttpPut("{id:guid}/updatedAt")]
    // public async Task<ActionResult> UpdateUpdatedAt(Guid id)
    // {
    //     var updated = await _service.UpdateUpdatedAtAsync(id);
    //     return Ok(updated);
    // }

    [HttpPut("close/{todoId:guid}")]
    public async Task<ActionResult> UpdateClosedAt(Guid todoId)
    {
        var updated = await _service.UpdateClosedAtAsync(todoId);
        return Ok(updated);
    }
}
