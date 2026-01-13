using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public class ClientService(IClientRepository clientRepository, IVisitRepository visitRepository) : IClientService
{

    public async Task<PagedRecords<ClientDto>> GetClientsAsync(int page, int pageSize, CancellationToken ct)
    {

        var clients = await clientRepository.GetClientsAsync(page, pageSize, ct);
        var a = clients.Select(c => new ClientDto()
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Surname = c.Surname,
            BirthDate = c.BirthDate,
        });

        var count = await clientRepository.GetClientCountAsync(ct);
        
        return new PagedRecords<ClientDto>
        {
            Records = a,
            Page =  page,
            PageSize = pageSize,
            TotalCount = count
        };
    }


    public async Task<ClientDetailedDto> GetClientByIdAsync(int id, CancellationToken ct)
    {
        var client = await clientRepository.GetClientByIdAsync(id, ct);

        if (client == null)
        {
            throw new NotFoundException($"Client with id {id} was not found!");
        }
        
        var result = new ClientDetailedDto()
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Surname = client.Surname,
            BirthDate = client.BirthDate
        };
        return result;
    }

    public async Task CreateClientAsync(ClientRequest clientRequest, CancellationToken ct)
    {
        if (clientRequest == null)
        {
            throw new BadRequestException("Client request is required!");
        }

        if (string.IsNullOrWhiteSpace(clientRequest.Name))
        {
            throw new BadRequestException("Name is required!");
        }

        if (string.IsNullOrWhiteSpace(clientRequest.Surname))
        {
            throw new BadRequestException("Surname is required!");
        }
        
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - clientRequest.BirthDate.Year;

        if (clientRequest.BirthDate > today.AddYears(-age))
        {
            age--;
        }

        if (age < 18)
        {
            throw new BadRequestException("Client must be at least 18 years old.");
        }        
        
        var client = new Client()
        {
            Name = clientRequest.Name, 
            Surname = clientRequest.Surname, 
            BirthDate = clientRequest.BirthDate
        };
        
        await clientRepository.AddClientAsync(client, ct);
    }

    public async Task UpdateClientAsync(int clientId, ClientRequest? clientRequest, CancellationToken ct)
    {
        if(clientRequest == null)
            throw new BadRequestException("Client request is required!");

        if (clientRequest.BirthDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new BadRequestException("Birth date cannot be in the future!");
        }
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        if (clientRequest.BirthDate.AddYears(18) > today)
        {
            throw new BadRequestException("Client must be at least 18 years old");
        }

        var client = new Client()
        {
            ClientId = clientId,
            Name = clientRequest.Name,
            Surname = clientRequest.Surname,
            BirthDate = clientRequest.BirthDate
        };
        await clientRepository.UpdateClientAsync(client, ct);
    }

    public async Task DeleteClientAsync(int clientId, CancellationToken ct)
    {
        var existingClient = await clientRepository.GetClientByIdAsync(clientId, ct);
        if (existingClient == null)
        {
            throw new NotFoundException($"Client with id {clientId} was not found!");
        }
        
        var visits = await visitRepository.GetVisitsByClientId(clientId, ct);
        if (visits.Any())
        {
            throw new BadRequestException($"Client with id {clientId} has visits!");
        }

        await clientRepository.DeleteClientAsync(clientId, ct);
    }

    public async Task<UserDto> GetClientByUserIdAsync(int userId, CancellationToken ct)
    {
        var client = await clientRepository.GetClientByUserIdAsync(userId, ct)
            ?? throw new NotFoundException($"Client with user id {userId} was not found!");

        return new UserDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Surname = client.Surname,
            BirthDate = client.BirthDate,
            Email = client.User.Email
        };

    }
}