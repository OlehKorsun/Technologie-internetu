namespace WebApplication1.DTOs;

public class VisitDetailedDto
{
    public int VisitId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Comment { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public int BarberId { get; set; }
    public string BarberName { get; set; }
    public decimal Price { get; set; }
}