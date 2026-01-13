using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class BarberRepository : IBarberRepository
{
    private readonly AppDbContext _context;

    public BarberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Barber>> GetAllBarbersAsync(int page, int pageSize, CancellationToken ct)
    {
        var barbers = await _context.Barbers
            .OrderBy(b => b.BarberId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return barbers;
    }

    public async Task<Barber?> GetBarberByIdAsync(int id, CancellationToken ct)
    {
        var barber = await _context.Barbers.FindAsync([id], ct);
        return barber;
    }

    public async Task AddBarberAsync(Barber barber, CancellationToken ct)
    {
        await _context.Barbers.AddAsync(barber, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateBarberAsync(Barber barber, CancellationToken ct)
    {
        _context.Barbers.Update(barber);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteBarberAsync(Barber barber, CancellationToken ct)
    {
        _context.Barbers.Remove(barber);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<int> GetBarberCountAsync(CancellationToken ct)
    {
        var count = await _context.Barbers.CountAsync(ct);
        return count;
    }
}