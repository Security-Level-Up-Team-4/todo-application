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
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var todo = await _service.GetByIdAsync(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult> Create(TodosDTO dto)
    {
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, TodosDTO dto)
    {
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
