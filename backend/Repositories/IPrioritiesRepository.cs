using backend.Models;

namespace backend.Repositories;

public interface IPrioritiesRepository
{
    Task<IEnumerable<Priorities>> GetAllAsync();
    Task<Priorities?> GetByIdAsync(int id);
    Task<Priorities?> GetByNameAsync(string name);
}
