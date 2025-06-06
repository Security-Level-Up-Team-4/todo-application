using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface IPrioritiesService
{
    Task<IEnumerable<Priorities>> GetAllAsync();
    Task<Priorities?> GetByIdAsync(int id);
    Task<Priorities> CreateAsync(string priorityName);
    Task<Priorities?> UpdateAsync(int id, string updatedPriority);
}
