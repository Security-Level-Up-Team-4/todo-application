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
    [Authorize(Roles = "access_admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "access_admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRoleDto updateUserDto)
    {
        try
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
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
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


