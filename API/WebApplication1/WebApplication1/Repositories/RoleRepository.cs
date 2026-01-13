using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetRoleById(int roleId, CancellationToken ct)
    {
        var role = await _context.Roles.FindAsync([roleId], ct);
        return role;
    }

    public async Task<Role?> GetRoleByTitle(string roleTitle, CancellationToken ct)
    {
        var role = await _context.Roles.FirstOrDefaultAsync((r => r.Title == roleTitle), ct);
        return role;
    }
}