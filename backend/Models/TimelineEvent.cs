using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models;

public class TimelineEvent
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("todo_id")]
    public Guid TodoId { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Column("event_type")]
    public string EventType { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
