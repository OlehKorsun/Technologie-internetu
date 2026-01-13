using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<User?> GetUserByLoginAsync(string login, CancellationToken ct)
    {
        return await _context.Users
            .Include(u => u.IdRolaNavigation)
            .FirstOrDefaultAsync((u => u.Login == login), ct);
    }

    public async Task AddUserAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
    }

}