using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class RegisterUserRequest
{
    [Required]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
    
    
    [MaxLength(50)]
    public string? RoleTitle { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
}