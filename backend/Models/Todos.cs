using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Todos
{
    [Key]
    public Guid id { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [ForeignKey("Priority")]
    [Column("priority_id")]
    public int PriorityId { get; set; }

    [ForeignKey("TaskStatus")]
    [Column("status_id")]
    public int StatusId { get; set; }
    [Column("created_by")]
    public Guid CreatedBy { get; set; }

    [ForeignKey("Team")]
    [Column("team_id")]
    public Guid TeamId { get; set; }
    [Column("assigned_to")]
    public Guid? assigned_to { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }
}
