using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult> GetById(Guid id)
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var todo = await _service.GetByIdAsync(id, userId);
            if (todo == null)
                return NotFound(new { message = "Todo does not exist" });
            return Ok(todo);
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

    [HttpPost("{teamId:guid}")]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult> Create([FromBody] TodosDTO dto, Guid teamId)
    {
        try
        {
            var userId = _userContextService.GetUserId();

            var newTodo = await _service.AddAsync(dto, userId, teamId);
            return CreatedAtAction(nameof(Create), new { id = newTodo.id }, newTodo);
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

    [HttpPut("assign/{todoId:guid}")]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult> UpdateAssignedTo(Guid todoId)
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var updated = await _service.UpdateAssignedToAsync(todoId, userId);
            return Ok(updated);
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

    [HttpPut("unassign/{todoId:guid}")]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult> Unassign(Guid todoId)
    {
        try
        {
            var userId = _userContextService.GetUserId();
            var updated = await _service.UnassignAsync(todoId, userId);
            return Ok(updated);
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

    [HttpPut("close/{todoId:guid}")]
    [Authorize(Roles = "team_lead,todo_user")]
    public async Task<ActionResult> UpdateClosedAt(Guid todoId)
    {
        try
        {
            var updated = await _service.UpdateClosedAtAsync(todoId);
            return Ok(updated);
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
