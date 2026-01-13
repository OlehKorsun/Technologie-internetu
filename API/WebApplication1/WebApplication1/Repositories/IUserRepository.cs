using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByLoginAsync(string login, CancellationToken ct);
    Task AddUserAsync(User user, CancellationToken ct);
}