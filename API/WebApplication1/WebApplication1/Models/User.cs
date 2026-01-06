using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public partial class User
{
    [Key]
    public int IdUser { get; set; }

    [Required]
    [MaxLength(50)]
    public string Login { get; set; } = null!;

    [Required]
    [MaxLength(256)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string Salt { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [ForeignKey(nameof(IdRolaNavigation))]
    public int IdRola { get; set; }

    public virtual Role IdRolaNavigation { get; set; } = null!;
    
    public Client? Client { get; set; }
}