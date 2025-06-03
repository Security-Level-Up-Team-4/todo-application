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
    public async Task<ActionResult<Priorities>> Create(PrioritiesDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Name }, created);
    }
}
