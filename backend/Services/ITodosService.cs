using backend.DTOs;
using backend.Models;

namespace backend.Services;

public interface ITodosService
{
    Task<List<Todos>> GetAllAsync();
    Task<TodosDTO?> GetByIdAsync(Guid todoId, Guid userId);
    Task<TeamDetailsDto?> GetByTeamIdAsync(Guid teamId, Guid userId);
    Task<TodosDTO> AddAsync(TodosDTO dto, Guid userId, Guid teamId);
    Task<Todos?> UpdateTitleAsync(Guid todoId, string title);
    Task<Todos?> UpdateDescriptionAsync(Guid todoId, string description);
    Task<Todos?> UpdatePriorityAsync(Guid todoId, int priorityId);
    Task<Todos?> UpdateStatusAsync(Guid todoId, int statusId);
    Task<Todos?> UpdateTeamAsync(Guid todoId, Guid teamId);
    Task<TodosDTO?> UpdateAssignedToAsync(Guid todoId, Guid assignedTo);
    Task<TodosDTO?> UnassignAsync(Guid todoName, Guid assignedTo);
    Task<Todos?> UpdateUpdatedAtAsync(Guid todoId);
    Task<TodosDTO?> UpdateClosedAtAsync(Guid todoId, Guid userId);
    
    Task<TimelineEventDto>GetTimelineByTodoIdAsync(Guid todoId);
}
