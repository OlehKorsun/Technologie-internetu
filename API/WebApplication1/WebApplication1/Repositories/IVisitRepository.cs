using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Repositories;

public interface IVisitRepository
{
    Task<List<Visit>> GetAllVisitsAsync();
    Task<Visit?> GetVisitAsync(int visitId);
    Task<IEnumerable<Visit>> GetVisitsByBarberId(int barberId);
    Task<IEnumerable<Visit>> GetVisitsByClientId(int clientId);
    Task AddVisitAsync(Visit visit);
    Task UpdateVisitAsync(Visit visit);
    Task DeleteVisitAsync(int visitId);
    Task<bool> BarberHasOverlappingVisitAsync(
        int barberId,
        DateTime start,
        DateTime end,
        int? excludedVisitId = null);

}