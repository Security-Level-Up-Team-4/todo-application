using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _userRepository;

    public UsersService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user;
    }
}
