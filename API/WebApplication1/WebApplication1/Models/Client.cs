using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int ClientId {get;set;}
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Surname { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    public ICollection<Visit> Visits { get; set; }
    
    public User? User { get; set; }
    public int? UserId { get; set; }
}