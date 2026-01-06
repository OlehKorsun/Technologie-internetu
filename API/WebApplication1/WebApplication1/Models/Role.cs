using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Role
{
    [Key]
    public int IdRole { get; set; }

    [Required]
    [MaxLength(25)]
    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}