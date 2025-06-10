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
        var todo = await _service.GetByIdAsync(id, userId);
        return Ok(todo);
    }

    [HttpPost("{teamId:guid}")]
    public async Task<ActionResult> Create([FromBody] TodosDTO dto, Guid teamId)
    {

        var userId = _userContextService.GetUserId();

        var newTodo = await _service.AddAsync(dto, userId, teamId);
        return CreatedAtAction(nameof(Create), new { id = newTodo.id }, newTodo);
    }

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

    [HttpPut("close/{todoId:guid}")]
    public async Task<ActionResult> UpdateClosedAt(Guid todoId)
    {
        var userId = _userContextService.GetUserId();
        var updated = await _service.UpdateClosedAtAsync(todoId, userId);
        return Ok(updated);
    }
    
    [HttpGet("timeline")]
    public async Task<IActionResult> GetTimelineByTodoId([FromQuery] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Todo ID is required.");

        var timeline = await _service.GetTimelineByTodoIdAsync(id);
        return Ok(timeline);
    }
}
