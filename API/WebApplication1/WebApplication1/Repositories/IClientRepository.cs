using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IClientRepository
{
    Task<List<Client>> GetClientsAsync(int page, int pageSize, CancellationToken ct);
    Task<Client?> GetClientByIdAsync(int id, CancellationToken ct);
    Task AddClientAsync(Client client, CancellationToken ct);
    Task UpdateClientAsync(Client client, CancellationToken ct);
    Task DeleteClientAsync(int clientId, CancellationToken ct);
    Task<Client?> GetClientByUserIdAsync(int userId, CancellationToken ct);
    Task<int> GetClientCountAsync(CancellationToken ct);
}