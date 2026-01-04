using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IVisitService
{
    Task<IEnumerable<VisitDto>> GetAllVisits();
    Task<VisitDetailedDto> GetVisit(int visitId);
    Task<IEnumerable<VisitDto>> GetVisitsByClientId(int clientId);
    Task<IEnumerable<VisitDto>> GetVisitsByBarberId(int barberId);
    Task<VisitDto> CreateVisit(VisitRequest visitRequest);
    Task UpdateVisit(int visitId, VisitRequest visitRequest);
    Task DeleteVisit(int visitId);
}