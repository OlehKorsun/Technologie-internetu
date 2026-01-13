using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IBarberRepository
{
    Task<List<Barber>> GetAllBarbersAsync(int page, int pageSize, CancellationToken ct);
    Task<Barber?> GetBarberByIdAsync(int id, CancellationToken ct);
    Task AddBarberAsync(Barber barber, CancellationToken ct);
    Task UpdateBarberAsync(Barber barber, CancellationToken ct);
    Task DeleteBarberAsync(Barber barber, CancellationToken ct);
    Task<int> GetBarberCountAsync(CancellationToken ct);
}