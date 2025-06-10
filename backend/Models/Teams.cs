using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace backend.Models;

public class Teams
{
    [Key]
    [Column("id")]
    [JsonPropertyName("teamId")]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    [Column("name")]
    [JsonPropertyName("teamName")]
    public string Name { get; set; }

    [Required]
    [Column("created_by")]
    public Guid CreatedBy { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
