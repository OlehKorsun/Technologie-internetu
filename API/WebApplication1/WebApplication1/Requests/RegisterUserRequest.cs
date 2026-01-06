using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class RegisterUserRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}