using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public class VisitService : IVisitService
{
    private readonly IVisitRepository _visitRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IBarberRepository _barberRepository;

    public VisitService(IVisitRepository visitRepository, IClientRepository clientRepository, IBarberRepository barberRepository)
    {
        _visitRepository = visitRepository;
        _clientRepository = clientRepository;
        _barberRepository = barberRepository;
    }

    public async Task<IEnumerable<VisitDto>> GetAllVisits()
    {
        var visits = await _visitRepository.GetAllVisitsAsync();
        return visits.Select(v => new VisitDto()
        {
            VisitId = v.VisitId,
            Start = v.Start,
            End = v.End,
            ClientName = v.Client.Name,
            BarberName = v.Barber.Name,
            Price = v.Price
        });
    }

    public async Task<VisitDetailedDto> GetVisit(int visitId)
    {
        var visit = await _visitRepository.GetVisitAsync(visitId);
        if (visit == null)
        {
            throw new NotFoundException($"Visit with id {visitId} was not found!");
        }
        return new VisitDetailedDto()
        {
            VisitId = visit.VisitId,
            Start = visit.Start,
            End = visit.End,
            ClientId = visit.Client.ClientId,
            BarberId = visit.Barber.BarberId,
            Price = visit.Price,
            Comment = visit.Comment,
        };
    }

    public async Task<IEnumerable<VisitDto>> GetVisitsByClientId(int clientId)
    {
        var visits = await _visitRepository.GetVisitsByClientId(clientId);
        return visits.Select(v => new VisitDto()
        {
            VisitId = v.VisitId,
            Start = v.Start,
            End = v.End,
            ClientName = v.Client.Name,
            BarberName = v.Barber.Name,
            Price = v.Price
        });
    }

    public async Task<IEnumerable<VisitDto>> GetVisitsByBarberId(int barberId)
    {
        var visits = await _visitRepository.GetVisitsByBarberId(barberId);
        return visits.Select(v => new VisitDto()
        {
            Start = v.Start,
            End = v.End,
            ClientName = v.Client.Name,
            BarberName = v.Barber.Name,
            Price = v.Price
        });
    }

    public async Task<VisitDto> CreateVisit(VisitRequest visitRequest)        // Sprawdzić czy dobrze zrobione, czy wstawiać id czy obiekt
    {
        if (visitRequest == null)
            throw new BadRequestException("Visit request is required!");
        
        var client = await _clientRepository.GetClientByIdAsync(visitRequest.ClientId);
        if (client == null)
            throw new BadRequestException($"Client with id {visitRequest.ClientId} does not exist!");
        
        var barber = await _barberRepository.GetBarberByIdAsync(visitRequest.BarberId);
        if (barber == null)
            throw new BadRequestException($"Barber with id {visitRequest.BarberId} does not exist!");
        
        
        var visit = new Visit()
        {
            Start = visitRequest.Start,
            End = visitRequest.End,
            ClientId = visitRequest.ClientId,
            BarberId = visitRequest.BarberId,
            Comment = visitRequest.Comment,
            Price = visitRequest.Price
        };
        
        await _visitRepository.AddVisitAsync(visit);
        return new VisitDto()
        {
            VisitId = visit.VisitId,
            Start = visit.Start,
            End = visit.End,
            ClientName = client.Name,
            BarberName = barber.Name,
            Price = visit.Price,
        };
    }

    public async Task UpdateVisit(int visitId, VisitRequest visitRequest)
    {
        var existingVisit = await _visitRepository.GetVisitAsync(visitId);
        if (existingVisit == null)
        {
            throw new NotFoundException($"Visit with id {visitId} was not found!");
        }
        
        existingVisit.Start = visitRequest.Start;
        existingVisit.End = visitRequest.End;
        existingVisit.ClientId = visitRequest.ClientId;
        existingVisit.BarberId = visitRequest.BarberId;
        existingVisit.Comment = visitRequest.Comment;
        existingVisit.Price = visitRequest.Price;
        
        // var visit = new Visit()
        // {
        //     VisitId = visitId,
        //     Start = visitRequest.Start,
        //     End = visitRequest.End,
        //     ClientId = visitRequest.ClientId,
        //     BarberId = visitRequest.BarberId,
        //     Comment = visitRequest.Comment,
        //     Price = visitRequest.Price
        // };
        await _visitRepository.UpdateVisitAsync(existingVisit);
    }

    public async Task DeleteVisit(int visitId)
    {
        var existingVisit = await _visitRepository.GetVisitAsync(visitId);
        if (existingVisit == null)
        {
            throw new NotFoundException($"Visit with id {visitId} was not found!");
        }
        await _visitRepository.DeleteVisitAsync(visitId);
    }
    
}