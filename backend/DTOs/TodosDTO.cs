namespace backend.DTOs;

public class TodosDTO
{
    public Guid id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int priority { get; set; }
    public string priorityName { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public string? createdBy { get; set; }
    public Guid teamId { get; set; }
    public string? assignedTo { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public DateTime? closedAt { get; set; }


}
