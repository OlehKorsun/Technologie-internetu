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

    public async Task<List<Barber>> GetAllBarbersAsync()
    {
        var barbers = await _context.Barbers.ToListAsync();
        return barbers;
    }

    public async Task<Barber?> GetBarberByIdAsync(int id)
    {
        var barber = await _context.Barbers.FindAsync(id);
        return barber;
    }

    public async Task AddBarberAsync(Barber barber)
    {
        await _context.Barbers.AddAsync(barber);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBarberAsync(Barber barber)
    {
        _context.Barbers.Update(barber);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBarberAsync(Barber barber)
    {
        _context.Barbers.Remove(barber);
        await _context.SaveChangesAsync();
    }
}