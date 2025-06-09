public class TimelineEventDto
{
    public string todoName { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string PerformedBy { get; set; } = string.Empty; // Username or email
}
