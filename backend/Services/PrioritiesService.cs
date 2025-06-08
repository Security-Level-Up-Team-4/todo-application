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

    public async Task<Priorities> CreateAsync(string priorityName)
    {
        var existingPriority = await _repository.GetByNameAsync(priorityName);
        if (existingPriority != null)
        {
            throw new InvalidOperationException($"Priority '{priorityName}' already exists.");
        }

        var priority = new Priorities { Name = priorityName };
        var created = await _repository.CreateAsync(priority);
        return created;
    }

    public async Task<Priorities?> UpdateAsync(int id, string updatedPriority)
    {
        if (string.IsNullOrWhiteSpace(updatedPriority))
        {
            throw new ArgumentException("Updated priority name cannot be null or empty.", nameof(updatedPriority));
        }

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Priority with ID {id} not found.");
        }

        var existingPriority = await _repository.GetByNameAsync(updatedPriority);
        if (existingPriority != null )
        {
            throw new InvalidOperationException($"Priority '{updatedPriority}' already exists.");
        }

        existing.Name = updatedPriority;
        var updated = await _repository.UpdateAsync(id, updatedPriority);
        return updated;
    }
}
