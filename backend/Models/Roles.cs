using System.ComponentModel.DataAnnotations;
namespace backend.Models;

public class Roles
{
    [Key]
    public virtual int Id { get; set; }

    [Required, MaxLength(50)]
    public virtual string Name { get; set; }

}
