using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> GetAllAsync() =>
        await _context.User.ToListAsync();

    public async Task<Users?> GetByIdAsync(Guid id) =>
        await _context.User.FindAsync(id);

    public async Task<Users?> GetByEmailAsync(string email) =>
        await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    
     public async Task<Users?> GetByUserNameAsync(string username) =>
        await _context.User.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
