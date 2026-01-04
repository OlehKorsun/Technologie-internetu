namespace WebApplication1.DTOs;

public class BarberDetailedDto
{
    public int BarberId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
}