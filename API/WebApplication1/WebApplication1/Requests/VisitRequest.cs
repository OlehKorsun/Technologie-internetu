namespace WebApplication1.Requests;

public class VisitRequest
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Price { get; set; }
    public int ClientId { get; set; }
    public int BarberId { get; set; }
    public string Comment { get; set; }
}