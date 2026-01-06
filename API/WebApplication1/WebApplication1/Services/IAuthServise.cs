using WebApplication1.Requests;

namespace WebApplication1.Services;

public interface IAuthServise
{
    Task<String> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterUserRequest request);
}