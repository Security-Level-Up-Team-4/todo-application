public class TimelineEventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<TodoTimelineDto> Timeline { get; set; } = new List<TodoTimelineDto>();
}

public class TodoTimelineDto
{
    public string Event { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
