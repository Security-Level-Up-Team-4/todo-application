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

    public async Task<MembershipStatus?> GetStatusByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

}
