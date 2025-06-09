using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRoleDto updateUserDto)
    {
        if (id == Guid.Empty)
            return BadRequest(new { message = "User ID cannot be empty." });

        if (string.IsNullOrEmpty(updateUserDto.RoleName))
            return BadRequest(new { message = "Role is required" });

        var user = await _userService.updateUserRoleAsync(id, updateUserDto.RoleName);
        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user);
    }
}


