using backend.DTOs;
using backend.Models;
namespace backend.Services;

public interface IUsersService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    
}
