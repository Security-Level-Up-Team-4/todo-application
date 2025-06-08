using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models;

public class Teams
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }

    [Required]
    [Column("created_by")]
    public Guid CreatedBy { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
