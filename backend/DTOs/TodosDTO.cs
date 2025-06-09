namespace backend.DTOs;

public class TodosDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PriorityId { get; set; }
    public string PriorityName { get; set; } = string.Empty;
}
