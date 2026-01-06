using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;
using WebApplication1.Services;
using LoginRequest = WebApplication1.Requests.LoginRequest;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthServise _authServise;

    public AuthController(IConfiguration config, IAuthServise authServise)
    {
        _config = config;
        _authServise = authServise;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return Ok();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        return Ok("Rejestracja przebiegła prawidłowo");
    }
}