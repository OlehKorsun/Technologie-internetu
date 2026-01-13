namespace WebApplication1.DTOs;

public class UserDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } 
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; }
}