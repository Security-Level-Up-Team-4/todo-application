using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriorityController : ControllerBase
{
    private readonly IPrioritiesService _service;

    public PriorityController(IPrioritiesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Priorities>>> GetAll()
    {
        var priorities = await _service.GetAllAsync();
        return Ok(priorities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Priorities>> GetById(int id)
    {
        var priority = await _service.GetByIdAsync(id);
        if (priority == null) return NotFound();
        return Ok(priority);
    }

    [HttpPost]
    public async Task<ActionResult<Priorities>> Create(string priorityName)
    {
        if (string.IsNullOrWhiteSpace(priorityName))
        {
            return BadRequest("Priority name cannot be null or empty.");
        }

        var created = await _service.CreateAsync(priorityName);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Priorities>> Update(int id, string updatedPriority)
    {
        if (string.IsNullOrWhiteSpace(updatedPriority))
        {
            return BadRequest("Updated priority name cannot be null or empty.");
        }

        try
        {
            var updated = await _service.UpdateAsync(id, updatedPriority);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
  
}
