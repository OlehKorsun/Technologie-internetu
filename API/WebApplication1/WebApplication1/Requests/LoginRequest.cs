using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class LoginRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Required]
    [MinLength(2)]
    public string Password { get; set; }
}