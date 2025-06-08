using backend.DTOs;
using backend.Models;
namespace backend.Services;

public interface IUsersService
{
    Task<IEnumerable<UsersDTO>> GetAllAsync();
    Task<UsersDTO?> GetByIdAsync(Guid id);
    
}
