using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class VisitRequest
{
    [Required]
    public DateTime Start { get; set; }
    
    [Required]
    public DateTime End { get; set; }
    
    [Required]
    [Range(1, 2000, ErrorMessage = "Price must be between 1 and 2000.")]
    public decimal Price { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int BarberId { get; set; }
    
    [MaxLength(200)]
    public string Comment { get; set; }
}