using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IBarberRepository
{
    Task<List<Barber>> GetAllBarbersAsync();
    Task<Barber?> GetBarberByIdAsync(int id);
    Task AddBarberAsync(Barber barber);
    Task UpdateBarberAsync(Barber barber);
    Task DeleteBarberAsync(Barber barber);
}