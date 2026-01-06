using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AppDbContext _context;

    public VisitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Visit>> GetAllVisitsAsync()
    {
        var visits = await _context.Visits
            .Include(v => v.Client)
            .Include(v => v.Barber)
            .ToListAsync();
        return visits;
    }

    public async Task<Visit?> GetVisitAsync(int visitId)
    {
        var visit = await _context.Visits
            .Include(b => b.Barber)
            .Include(c => c.Client)
            .FirstOrDefaultAsync(v => v.VisitId == visitId);
        return visit;
    }

    public async Task<IEnumerable<Visit>> GetVisitsByBarberId(int barberId)
    {
        var visits = await _context.Visits
            .Include(c => c.Client)
            .Include(b => b.Barber)
            .Where(v => v.BarberId == barberId)
            .ToListAsync();
        return visits;
    }

    public async Task<IEnumerable<Visit>> GetVisitsByClientId(int clientId)
    {
        var visits = await _context.Visits
            .Include(c => c.Client)
            .Include(b => b.Barber)
            .Where(v => v.ClientId == clientId)
            .ToListAsync();
        return visits;
    }

    public async Task AddVisitAsync(Visit visit)
    {
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVisitAsync(Visit visit)
    {
        _context.Update(visit);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVisitAsync(int visitId)
    {
        var visit = await _context.Visits.FindAsync(visitId);
        _context.Visits.Remove(visit);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> BarberHasOverlappingVisitAsync(
        int barberId,
        DateTime start,
        DateTime end,
        int? excludedVisitId = null)
    {
        return await _context.Visits.AnyAsync(v =>
            v.BarberId == barberId &&
            (excludedVisitId == null || v.VisitId != excludedVisitId) &&
            v.Start < end &&
            v.End > start
        );
    }

}