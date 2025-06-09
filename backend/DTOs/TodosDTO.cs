namespace backend.DTOs;

public class TodosDTO
{
    public Guid id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int priority { get; set; }
    public string priorityName { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;

}
