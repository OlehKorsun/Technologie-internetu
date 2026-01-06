using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleById(int roleId);
    Task<Role?> GetRoleByTitle(string roleTitle);
}