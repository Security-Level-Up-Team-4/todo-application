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

    public async Task<MembershipStatus> CreateStatusAsync(MembershipStatusDto status)
    {
        await ValidateStatusNameIsUniqueAsync(status.Name);

        var newStatus = new MembershipStatus
        {
            Name = status.Name
        };

        return await _repository.AddAsync(newStatus);
    }

    public async Task<MembershipStatus?> GetStatusByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MembershipStatus?> UpdateStatusAsync(int id, MembershipStatusDto updatedStatus)
    {
        await ValidateStatusNameIsUniqueAsync(updatedStatus.Name);
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;//TODO: Handle not found case

        existing.Name = updatedStatus.Name;
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
