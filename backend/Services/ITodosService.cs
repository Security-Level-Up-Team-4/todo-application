using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface ITodosService
{
    Task<List<Todos>> GetAllAsync();
    Task<Todos?> GetByIdAsync(Guid todoId,Guid userId);
    Task<TeamDetailsDto?> GetByTeamIdAsync(Guid teamId,Guid userId);
    Task AddAsync(TodosDTO dto, Guid userId,Guid teamId);
    Task<Todos?> UpdateTitleAsync(Guid todoId, string title);
    Task<Todos?> UpdateDescriptionAsync(Guid todoId, string description);
    Task<Todos?> UpdatePriorityAsync(Guid todoId, int priorityId);
    Task<Todos?> UpdateStatusAsync(Guid todoId, int statusId);
    Task<Todos?> UpdateTeamAsync(Guid todoId, Guid teamId);
    Task<Todos?> UpdateAssignedToAsync(Guid todoId, Guid assignedTo);
    Task<Todos?> UnassignAsync(Guid todoName, Guid assignedTo);
    Task<Todos?> UpdateUpdatedAtAsync(Guid todoId);
    Task<Todos?> UpdateClosedAtAsync(Guid todoId);
}
