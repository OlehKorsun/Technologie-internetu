namespace WebApplication1.Models;

public class Role
{
    public int IdRole { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}