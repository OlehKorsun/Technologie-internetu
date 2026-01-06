using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class LoginRequest
{
    [Required]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}