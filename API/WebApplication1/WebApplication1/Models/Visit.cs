using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Visit")]
public class Visit
{
    [Key]
    public int VisitId { get; set; }
    
    [Required]
    public DateTime Start { get; set; }
    
    [Required]
    public DateTime End { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Comment { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    
    [ForeignKey(nameof(BarberId))]
    public Barber Barber { get; set; }
    
    public int ClientId { get; set; }
    
    public int BarberId { get; set; }
}