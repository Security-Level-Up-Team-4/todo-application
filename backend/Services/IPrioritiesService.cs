using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface IPrioritiesService
{
    Task<IEnumerable<Priorities>> GetAllAsync();
    Task<Priorities?> GetByIdAsync(int id);
    Task<Priorities> CreateAsync(PrioritiesDto dto);
}
