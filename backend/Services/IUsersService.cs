using backend.DTOs;
using backend.Models;
namespace backend.Services;

public interface IUsersService
{
    Task<IEnumerable<Users>> GetAllAsync();
    Task<Users?> GetByIdAsync(Guid id);
    
}
