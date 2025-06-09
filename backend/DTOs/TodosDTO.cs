namespace backend.DTOs;

public class TodosDTO
{
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int priority { get; set; }
    public string PriorityName { get; set; } = string.Empty;
}
