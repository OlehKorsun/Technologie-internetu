using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Repositories;

public interface IVisitRepository
{
    Task<List<Visit>> GetAllVisitsAsync(int page, int pageSize, CancellationToken ct);
    Task<Visit?> GetVisitAsync(int visitId, CancellationToken ct);
    Task<IEnumerable<Visit>> GetVisitsByBarberId(int barberId, CancellationToken ct);
    Task<IEnumerable<Visit>> GetVisitsByClientId(int clientId, CancellationToken ct);
    Task AddVisitAsync(Visit visit, CancellationToken ct);
    Task UpdateVisitAsync(Visit visit, CancellationToken ct);
    Task DeleteVisitAsync(int visitId, CancellationToken ct);
    Task<bool> BarberHasOverlappingVisitAsync(
        int barberId,
        DateTime start,
        DateTime end,
        CancellationToken ct,
        int? excludedVisitId = null);
    Task<IEnumerable<Visit>> GetVisitsByUserId(int clientId, CancellationToken ct);
    Task<Visit?> GetVisitByUserIdAsync (int visitId, int userId, CancellationToken ct);
    Task<int> GetVisitCountAsync(CancellationToken ct);
}