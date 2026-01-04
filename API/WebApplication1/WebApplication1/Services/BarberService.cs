using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public class BarberService(IBarberRepository barberRepository, IVisitRepository visitRepository) : IBarberService
{

    public async Task<IEnumerable<BarberDto>> GetBarbersAsync()
    {
        var barbers = await barberRepository.GetAllBarbersAsync();
        return barbers.Select(b => new BarberDto()
        {
            BarberId = b.BarberId,
            Name = b.Name,
            Surname = b.Surname,
        });

    }

    public async Task<BarberDetailedDto> GetBarberAsync(int barberId)
    {
        var barber = await barberRepository.GetBarberByIdAsync(barberId);
        if (barber == null)
        {
            throw new NotFoundException($"Barber with id {barberId} was not found!");
        }

        return new BarberDetailedDto()
        {
            BarberId = barber.BarberId,
            Name = barber.Name,
            Surname = barber.Surname,
            BirthDate = barber.BirthDate,
        };
    }


    public async Task CreateBarberAsync(BarberRequest? barberRequest)
    {
        if (barberRequest == null)
        {
            throw new BadRequestException("Barber request is required!");
        }

        if (string.IsNullOrWhiteSpace(barberRequest.Name))
        {
            throw new BadRequestException("Barber name is required!");
        }

        if (string.IsNullOrWhiteSpace(barberRequest.Surname))
        {
            throw new BadRequestException("Barber surname is required!");
        }
        
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - barberRequest.BirthDate.Year;

        if (barberRequest.BirthDate > today.AddYears(-age))
        {
            age--;
        }

        if (age < 18)
        {
            throw new BadRequestException("Barber must be at least 18 years old.");
        }

        var barber = new Barber()
        {
            Name = barberRequest.Name,
            Surname = barberRequest.Surname,
            BirthDate = barberRequest.BirthDate,
        };
        
        await barberRepository.AddBarberAsync(barber);
    }


    public async Task UpdateBarberAsync(int barberId, BarberRequest? barberRequest)
    {
        if (barberRequest == null)
        {
            throw new BadRequestException("Barber request is required!");
        }
        
        if (barberRequest.BirthDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new BadRequestException("Birth date cannot be in the future!");
        }
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        if (barberRequest.BirthDate.AddYears(18) > today)
        {
            throw new BadRequestException("Barber must be at least 18 years old");
        }

        var barber = new Barber()
        {
            BarberId = barberId,
            Name = barberRequest.Name,
            Surname = barberRequest.Surname,
            BirthDate = barberRequest.BirthDate,
        };
        await barberRepository.UpdateBarberAsync(barber);
    }

    public async Task DeleteBarberAsync(int barberId)
    {
        var barber = await barberRepository.GetBarberByIdAsync(barberId);
        if (barber == null)
            throw new NotFoundException($"Barber with id {barberId} was not found!");
        
        var visits = await visitRepository.GetVisitsByBarberId(barberId);
        if (visits.Any())
        {
            throw new BadRequestException($"Barber with id {barberId} has visits!");
        }
        
        await barberRepository.DeleteBarberAsync(barber);
    }
}