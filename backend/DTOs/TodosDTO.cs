namespace backend.DTOs;

public class TodosDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PriorityId { get; set; }
    public int StatusId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid TeamId { get; set; }
    public Guid? AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}
