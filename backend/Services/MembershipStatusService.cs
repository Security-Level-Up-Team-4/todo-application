using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class MembershipStatusService : IMembershipStatusService
{
    private readonly IMembershipStatusRepository _repository;

    public MembershipStatusService(IMembershipStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MembershipStatus>> GetAllStatusesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MembershipStatus> CreateStatusAsync(string status)
    {
        await ValidateStatusNameIsUniqueAsync(status);

        var newStatus = new MembershipStatus
        {
            Name = status
        };

        return await _repository.AddAsync(newStatus);
    }

    public async Task<MembershipStatus?> GetStatusByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MembershipStatus?> UpdateStatusAsync(int id, string updatedStatus)
    {
        await ValidateStatusNameIsUniqueAsync(updatedStatus);
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;//TODO: Handle not found case

        existing.Name = updatedStatus;
        await _repository.UpdateAsync(existing);
        return existing;
    }
    
    private async Task ValidateStatusNameIsUniqueAsync(string name)
    {
        var existing = await _repository.GetByNameAsync(name);
        if (existing != null)
        {
            throw new InvalidOperationException($"Membership status '{name}' already exists.");
        }
    }
}
