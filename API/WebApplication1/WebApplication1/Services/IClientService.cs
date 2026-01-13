using WebApplication1.DTOs;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IClientService
{
    Task<PagedRecords<ClientDto>> GetClientsAsync(int page, int pageSize, CancellationToken ct);
    Task<ClientDetailedDto> GetClientByIdAsync(int id, CancellationToken ct);
    Task CreateClientAsync(ClientRequest client, CancellationToken ct);
    Task UpdateClientAsync(int id, ClientRequest? clientRequest, CancellationToken ct);
    Task DeleteClientAsync(int clientId, CancellationToken ct);
    Task<UserDto> GetClientByUserIdAsync(int userId, CancellationToken ct);
}