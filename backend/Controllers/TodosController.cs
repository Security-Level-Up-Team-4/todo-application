using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodosService _service;

    public TodosController(ITodosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var todo = await _service.GetByIdAsync(id);
        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TodosDTO dto)
    {
        await _service.AddAsync(dto);
        return StatusCode(201);
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

    [HttpPut("{id:guid}/team/{teamId:guid}")]
    public async Task<ActionResult> UpdateTeam(Guid id, Guid teamId)
    {
        var updated = await _service.UpdateTeamAsync(id, teamId);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/assignedTo/{userId:guid}")]
    public async Task<ActionResult> UpdateAssignedTo(Guid id, Guid userId)
    {
        var updated = await _service.UpdateAssignedToAsync(id, userId);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/updatedAt")]
    public async Task<ActionResult> UpdateUpdatedAt(Guid id)
    {
        var updated = await _service.UpdateUpdatedAtAsync(id);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/closedAt")]
    public async Task<ActionResult> UpdateClosedAt(Guid id)
    {
        var updated = await _service.UpdateClosedAtAsync(id);
        return Ok(updated);
    }
}
