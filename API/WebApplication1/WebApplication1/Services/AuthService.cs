using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Requests;

namespace WebApplication1.Services;

public class AuthService : IAuthServise
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _configuration = configuration;
    }
    
    public async Task<String> LoginAsync(LoginRequest request)
    {
        Console.WriteLine(request.Login);
        Console.WriteLine(request.Password);
        var user = await _userRepository.GetUserByLoginAsync(request.Login);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var hash = ComputeHash(request.Password + user.Salt);
        Console.WriteLine($"Hash: ${hash}");
        if (user.Password != hash)
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }

        var token = GenerateJwt(user);
        return token;
    }


    public async Task RegisterAsync(RegisterUserRequest request)
    {
        if (request == null)
        {
            throw new BadRequestException("Register request is required!");
        }
        
        var user = await _userRepository.GetUserByLoginAsync(request.Login);
        if (user != null)
        {
            throw new UserExistsException("User with given login already exists!");
        }

        var hashSold = CreateSold();
        var hashPassword = ComputeHash(request.Password + hashSold);
        
        int roleId = 2;
        
        user = new User()
        {
            Login = request.Login,
            Password = hashPassword,
            Salt = hashSold,
            Email = request.Email,
            IdRola = roleId,
        };
        
        await _userRepository.AddUserAsync(user);
    }
    
    
    private string GenerateJwt(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Role, user.IdRolaNavigation.Title)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
    
    private static string CreateSold()
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string saltBase64 = Convert.ToBase64String(salt);
        return saltBase64;
    }
}