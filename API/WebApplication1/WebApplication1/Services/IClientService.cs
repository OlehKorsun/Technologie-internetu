using WebApplication1.DTOs;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetClientsAsync();
    Task<ClientDetailedDto> GetClientByIdAsync(int id);
    
    Task CreateClientAsync(ClientRequest client);
    Task UpdateClientAsync(int id, ClientRequest? clientRequest);

    Task DeleteClientAsync(int clientId);
}