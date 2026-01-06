using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByLoginAsync(string login);
    Task AddUserAsync(User user);
}