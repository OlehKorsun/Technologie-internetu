using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Barber")]
public class Barber
{
    [Key]
    public int BarberId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Surname { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    public ICollection<Visit> Visits { get; set; }
}