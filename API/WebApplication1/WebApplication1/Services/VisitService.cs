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
        
        var client = await _clientRepository.GetClientByIdAsync(visit.ClientId);
        if(client == null) throw new NotFoundException($"Client with id {visit.ClientId} was not found!");
        var barber = await _barberRepository.GetBarberByIdAsync(visit.BarberId);
        if(barber == null) throw new NotFoundException($"Barber with id {visit.BarberId} was not found!");
        
        
        return new VisitDetailedDto()
        {
            VisitId = visit.VisitId,
            Start = visit.Start,
            End = visit.End,
            ClientId = visit.Client.ClientId,
            ClientName = client.Name,
            BarberId = visit.Barber.BarberId,
            BarberName = barber.Name,
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
        
        if (visitRequest.Start >= visitRequest.End)
            throw new BadRequestException("Visit start must be before end");
        
        ValidateWorkingHours(visitRequest.Start, visitRequest.End);
        
        if(visitRequest.Price < 0)
            throw new BadRequestException("Visit price must not be less then 0!");
        
        var client = await _clientRepository.GetClientByIdAsync(visitRequest.ClientId);
        if (client == null)
            throw new BadRequestException($"Client with id {visitRequest.ClientId} does not exist!");
        
        var barber = await _barberRepository.GetBarberByIdAsync(visitRequest.BarberId);
        if (barber == null)
            throw new BadRequestException($"Barber with id {visitRequest.BarberId} does not exist!");
        
        var barberBusy = await _visitRepository
            .BarberHasOverlappingVisitAsync(
                visitRequest.BarberId,
                visitRequest.Start,
                visitRequest.End);

        if (barberBusy)
        {
            throw new BadRequestException("Barber already has visit at this time!");
        }
        
        
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
        if (visitRequest == null)
        {
            throw new BadRequestException("Visit request is required!");
        }
        
        var existingVisit = await _visitRepository.GetVisitAsync(visitId);
        if (existingVisit == null)
        {
            throw new NotFoundException($"Visit with id {visitId} was not found!");
        }

        if (visitRequest.Start >= visitRequest.End)
        {
            throw new BadRequestException("Visit start must be before end");
        }
        
        ValidateWorkingHours(visitRequest.Start, visitRequest.End);
        
        if(visitRequest.Price < 0)
            throw new BadRequestException("Visit price must not be less then 0!");
        
        var client = await _clientRepository.GetClientByIdAsync(visitRequest.ClientId);
        if (client == null)
            throw new BadRequestException($"Client with id {visitRequest.ClientId} does not exist!");
        
        var barber = await _barberRepository.GetBarberByIdAsync(visitRequest.BarberId);
        if (barber == null)
            throw new BadRequestException($"Barber with id {visitRequest.BarberId} does not exist!");
        
        var barberBusy = await _visitRepository
            .BarberHasOverlappingVisitAsync(
                visitRequest.BarberId,
                visitRequest.Start,
                visitRequest.End,
                visitId);

        if (barberBusy)
        {
            throw new BadRequestException("Barber already has visit at this time!");
        }
        
        existingVisit.Start = visitRequest.Start;
        existingVisit.End = visitRequest.End;
        existingVisit.ClientId = visitRequest.ClientId;
        existingVisit.BarberId = visitRequest.BarberId;
        existingVisit.Comment = visitRequest.Comment;
        existingVisit.Price = visitRequest.Price;
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
    
    private void ValidateWorkingHours(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
            throw new BadRequestException("Visit must be in one day");

        var day = start.DayOfWeek;

        if (day == DayOfWeek.Sunday)
            throw new BadRequestException("Barbershop is closed on Sundays");

        TimeSpan open;
        TimeSpan close;

        if (day == DayOfWeek.Saturday)
        {
            open = new TimeSpan(10, 0, 0);
            close = new TimeSpan(14, 0, 0);
        }
        else
        {
            open = new TimeSpan(9, 0, 0);
            close = new TimeSpan(18, 0, 0);
        }

        if (start.TimeOfDay < open || end.TimeOfDay > close)
            throw new BadRequestException(
                $"Barbershop works from {open} to {close}");
    }

    
}