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

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        return await _context.Users
            .Include(u => u.IdRolaNavigation)
            .FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

}