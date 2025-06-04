using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Todos
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [ForeignKey("Priority")]
    public int PriorityId { get; set; }

    [ForeignKey("TaskStatus")]
    public int StatusId { get; set; }

    public Guid CreatedBy { get; set; }

    [ForeignKey("Team")]
    public Guid TeamId { get; set; }

    public Guid? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
}
