using WebApplication1.DTOs;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IBarberService
{
    Task<IEnumerable<BarberDto>> GetBarbersAsync();
    Task<BarberDetailedDto> GetBarberAsync(int barberId);
    Task CreateBarberAsync(BarberRequest barberRequest);
    Task UpdateBarberAsync(int barberId, BarberRequest barberRequest);
    Task DeleteBarberAsync(int barberId);
}