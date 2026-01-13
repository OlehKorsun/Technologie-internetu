using WebApplication1.DTOs;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IBarberService
{
    Task<PagedRecords<BarberDto>> GetBarbersAsync(int page, int pageSize, CancellationToken ct);
    Task<BarberDetailedDto> GetBarberAsync(int barberId, CancellationToken ct);
    Task CreateBarberAsync(BarberRequest barberRequest, CancellationToken ct);
    Task UpdateBarberAsync(int barberId, BarberRequest barberRequest, CancellationToken ct);
    Task DeleteBarberAsync(int barberId, CancellationToken ct);
}