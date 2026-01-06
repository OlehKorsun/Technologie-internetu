using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Barber> Barbers { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.IdRolaNavigation)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.IdRola)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Client>()
            .HasOne(c => c.User)
            .WithOne(u => u.Client)
            .HasForeignKey<Client>(c => c.UserId);

        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new Client() { ClientId = 1, Name = "Mateusz", Surname = "Kowalczyk", BirthDate = new DateOnly(1991, 4, 18)},
            new Client() { ClientId = 2, Name = "Agnieszka", Surname = "Nowak", BirthDate = new DateOnly(1988, 9, 7)},
            new Client() { ClientId = 3, Name = "Krzysztof", Surname = "Zielinski", BirthDate = new DateOnly(1994, 12, 22)}
        });

        modelBuilder.Entity<Barber>().HasData(new List<Barber>()
        {
            new Barber() {BarberId = 1, Name = "Adam", Surname = "Kowalski", BirthDate = new DateOnly(1990, 5, 12)},
            new Barber() {BarberId = 2, Name = "Jakub", Surname = "Nowak", BirthDate = new DateOnly(1996, 7, 23)},
            new Barber() {BarberId = 3, Name = "Michal", Surname = "Wojcik", BirthDate = new DateOnly(2001, 1, 6)}
        });

        modelBuilder.Entity<Visit>().HasData(new List<Visit>()
        {
            new Visit() {
                VisitId = 1, ClientId = 1, BarberId = 1, 
                Start = new DateTime(2025, 1, 5, 15, 30, 0),
                End = new DateTime(2025, 1, 5, 16, 30, 0),
                Comment = "Strzyrzenie",
                Price = 110
            },
            
            new Visit()
            {
                VisitId = 2, ClientId = 2, BarberId = 1, 
                Start = new DateTime(2025, 2, 18, 10, 0, 0),
                End = new DateTime(2025, 2, 18, 11, 0, 0),
                Comment = "Golenie",
                Price = 75
            },
            
            new Visit()
            {
                VisitId = 3, ClientId = 3, BarberId = 3, 
                Start = new DateTime(2025, 3, 1, 17, 10, 0),
                End = new DateTime(2025, 3, 1, 18, 30, 0),
                Comment = "Strzyrzenie + golenie",
                Price = 150
            }
        });

        modelBuilder.Entity<Role>().HasData(new List<Role>()
        {
            new Role()
            {
                IdRole = 1,
                Title = "admin"
            },
            new Role()
            {
                IdRole = 2,
                Title = "user"
            }
        });

        modelBuilder.Entity<User>().HasData(new User()
        {
            IdUser = 1,
            Login = "admin",
            Email = "admin@admin.com",
            Password = "HASHED_PASSWORD",
            Salt = "HASHED_SALT",
            IdRola = 1
        });
    }
    
    
}