using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Requests;

public class BarberRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Surname { get; set; }
    
    [Required]
    
    public DateOnly BirthDate { get; set; }
}