using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskStatusesController : ControllerBase
{
    private readonly ITaskStatusesService _service;

    public TaskStatusesController(ITaskStatusesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskStatusesDTO>>> GetAll()
    {
        var statuses = await _service.GetAllAsync();
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskStatusesDTO>> GetById(int id)
    {
        var status = await _service.GetByIdAsync(id);
        if (status == null)
            return NotFound();

        return Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<TaskStatusesDTO>> Create(TaskStatusesDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskStatusesDTO dto)
    {
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
