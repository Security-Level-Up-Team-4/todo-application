using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models;

public class Priorities
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, MaxLength(50), Column("name")]
    public string Name { get; set; } = string.Empty;
}