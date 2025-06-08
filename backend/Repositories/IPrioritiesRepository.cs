using backend.Models;

namespace backend.Repositories;

public interface IPrioritiesRepository
{
    Task<IEnumerable<Priorities>> GetAllAsync();
    Task<Priorities?> GetByIdAsync(int id);
    Task<Priorities> CreateAsync(Priorities priority);
    Task<Priorities?> UpdateAsync(int id, string updatedPriority);
    Task<Priorities?> GetByNameAsync(string name);
}
