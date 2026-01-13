using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetClientsAsync(int page, int pageSize, CancellationToken ct)
    {
        var clients = await _context.Clients
            .OrderBy(c => c.UserId)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return clients;
    }

    public async Task<Client?> GetClientByIdAsync(int id, CancellationToken ct)
    {
        var client = await _context.Clients.FindAsync([id], ct);
        return client;
    }

    public async Task AddClientAsync(Client client, CancellationToken ct)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateClientAsync(Client client, CancellationToken ct)
    {
        _context.Update(client);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteClientAsync(int clientId, CancellationToken ct)
    {
        var client = await _context.Clients.FindAsync([clientId], ct);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Client?> GetClientByUserIdAsync(int userId, CancellationToken ct)
    {
        var client = await _context.Clients
            .Include(c => c.User)
            .FirstOrDefaultAsync((c => c.UserId == userId), ct);
        return client;
    }

    public async Task<int> GetClientCountAsync(CancellationToken ct)
    {
        var count = await _context.Clients.CountAsync(ct);
        return count;
    }
    
}