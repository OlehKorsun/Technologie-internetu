using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IClientRepository
{
    Task<List<Client>> GetClientsAsync();
    Task<Client?> GetClientByIdAsync(int id);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(int clientId);
}