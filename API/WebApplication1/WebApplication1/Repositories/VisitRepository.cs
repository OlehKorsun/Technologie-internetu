using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AppDbContext _context;

    public VisitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Visit>> GetAllVisitsAsync(int page, int pageSize, CancellationToken ct)
    {
        var visits = await _context.Visits
            .Include(v => v.Client)
            .Include(v => v.Barber)
            .OrderBy(v => v.VisitId)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return visits;
    }

    public async Task<Visit?> GetVisitAsync(int visitId, CancellationToken ct)
    {
        var visit = await _context.Visits
            .Include(b => b.Barber)
            .Include(c => c.Client)
            .FirstOrDefaultAsync((v => v.VisitId == visitId), ct);
        return visit;
    }

    public async Task<IEnumerable<Visit>> GetVisitsByBarberId(int barberId, CancellationToken ct)
    {
        var visits = await _context.Visits
            .Include(c => c.Client)
            .Include(b => b.Barber)
            .Where(v => v.BarberId == barberId)
            .ToListAsync(ct);
        return visits;
    }

    public async Task<IEnumerable<Visit>> GetVisitsByClientId(int clientId, CancellationToken ct)
    {
        var visits = await _context.Visits
            .Include(c => c.Client)
            .Include(b => b.Barber)
            .Where(v => v.ClientId == clientId)
            .ToListAsync(ct);
        return visits;
    }

    public async Task<IEnumerable<Visit>> GetVisitsByUserId(int userId, int page, int pageSize, CancellationToken ct)
    {
        var visits = await _context.Visits
            .Include(c => c.Client)
            .Include(b => b.Barber)
            .Where(v => v.Client.UserId == userId)
            .OrderBy(v => v.VisitId)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return visits;
    }

    public async Task AddVisitAsync(Visit visit, CancellationToken ct)
    {
        await _context.Visits.AddAsync(visit, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateVisitAsync(Visit visit, CancellationToken ct)
    {
        _context.Update(visit);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteVisitAsync(int visitId, CancellationToken ct)
    {
        var visit = await _context.Visits.FindAsync([visitId], ct);
        _context.Visits.Remove(visit);
        await _context.SaveChangesAsync(ct);
    }
    
    public async Task<bool> BarberHasOverlappingVisitAsync(
        int barberId,
        DateTime start,
        DateTime end,
        CancellationToken ct,
        int? excludedVisitId = null)
    {
        return await _context.Visits.AnyAsync((v =>
            v.BarberId == barberId &&
            (excludedVisitId == null || v.VisitId != excludedVisitId) &&
            v.Start < end &&
            v.End > start
        ), ct);
    }

    public async Task<Visit?> GetVisitByUserIdAsync(int visitId, int userId, CancellationToken ct)
    {
        var visit = await _context.Visits
            .Include(v => v.Client)
            .Include(v => v.Barber)
            .FirstOrDefaultAsync((v => v.VisitId == visitId && v.Client.UserId == userId), ct);

        return visit;
    }

    public async Task<int> GetVisitCountAsync(CancellationToken ct)
    {
        var count = await _context.Visits.CountAsync(ct);
        return count;
    }

    public async Task<int> GetVisitCountByUserIdAsync(int userId, CancellationToken ct)
    {
        var count = await _context.Visits
            .Where(v => v.Client.UserId == userId)
            .CountAsync(ct);
        return count;
    }
}