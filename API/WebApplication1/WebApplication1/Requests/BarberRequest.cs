namespace WebApplication1.Requests;

public class BarberRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
}