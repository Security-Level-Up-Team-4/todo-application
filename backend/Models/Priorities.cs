using System.ComponentModel.DataAnnotations;
namespace backend.Models;

public class Priorities
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}