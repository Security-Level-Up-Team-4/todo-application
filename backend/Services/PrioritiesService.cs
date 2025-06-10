using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class PrioritiesService : IPrioritiesService
{
    private readonly IPrioritiesRepository _repository;

    public PrioritiesService(IPrioritiesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Priorities>> GetAllAsync()
    {
        var priorities = await _repository.GetAllAsync();
        return priorities;
    }

    public async Task<Priorities?> GetByIdAsync(int id)
    {
        var priority = await _repository.GetByIdAsync(id);
        return priority == null ? null : priority;
    }

}
