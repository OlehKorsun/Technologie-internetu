using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public class ClientService(IClientRepository clientRepository, IVisitRepository visitRepository) : IClientService
{

    public async Task<IEnumerable<ClientDto>> GetClientsAsync()
    {

        var clients = await clientRepository.GetClientsAsync();
        
        return clients.Select(c => new ClientDto()
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Surname = c.Surname,
            BirthDate = c.BirthDate,
        });
    }


    public async Task<ClientDetailedDto> GetClientByIdAsync(int id)
    {
        var client = await clientRepository.GetClientByIdAsync(id);

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

    public async Task CreateClientAsync(ClientRequest clientRequest)
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
        
        await clientRepository.AddClientAsync(client);
    }

    public async Task UpdateClientAsync(int clientId, ClientRequest? clientRequest)
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
        await clientRepository.UpdateClientAsync(client);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var existingClient = await clientRepository.GetClientByIdAsync(clientId);
        if (existingClient == null)
        {
            throw new NotFoundException($"Client with id {clientId} was not found!");
        }
        
        var visits = await visitRepository.GetVisitsByClientId(clientId);
        if (visits.Any())
        {
            throw new BadRequestException($"Client with id {clientId} has visits!");
        }

        await clientRepository.DeleteClientAsync(clientId);
    }
}