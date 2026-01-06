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

    public async Task<Role?> GetRoleById(int roleId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        return role;
    }

    public async Task<Role?> GetRoleByTitle(string roleTitle)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Title == roleTitle);
        return role;
    }
}