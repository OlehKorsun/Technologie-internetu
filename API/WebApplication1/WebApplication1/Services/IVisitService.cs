using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IVisitService
{
    Task<PagedRecords<VisitDto>> GetAllVisits(int page, int pageSize, CancellationToken ct);
    Task<VisitDetailedDto> GetVisit(int visitId, CancellationToken ct);
    Task<IEnumerable<VisitDto>> GetVisitsByClientId(int clientId, CancellationToken ct);
    Task<IEnumerable<VisitDto>> GetVisitsByBarberId(int barberId, CancellationToken ct);
    Task<VisitDto> CreateVisit(VisitRequest visitRequest, CancellationToken ct);
    Task UpdateVisit(int visitId, VisitRequest visitRequest, CancellationToken ct);
    Task DeleteVisit(int visitId, CancellationToken ct);
    Task<PagedRecords<VisitDto>> GetVisitsByUserId(int userId, int page, int pageSize, CancellationToken ct);
    Task<VisitDetailedDto> GetVisitByUserId(int visitId, int clientId, CancellationToken ct);
}