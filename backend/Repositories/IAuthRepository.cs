using backend.Models;
using System;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
        
    }
}
