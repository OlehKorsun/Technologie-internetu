using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetClientsAsync(
        int page = 1,
        int pageSize = 5,
        CancellationToken ct = default)
    {
        var result = await _clientService.GetClientsAsync(page, pageSize, ct);
        return Ok(result);
    }

    [HttpGet("{clientId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetClientByIdAsync([FromRoute]int clientId, CancellationToken ct = default)
    {
        var result = await _clientService.GetClientByIdAsync(clientId, ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateClientAsync([FromBody] ClientRequest client, CancellationToken ct = default)
    {
        await _clientService.CreateClientAsync(client, ct);
        return Created();
    }

    [HttpPut("{clientId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateClientAsync([FromRoute]int clientId, [FromBody]ClientRequest clientRequest, CancellationToken ct = default)
    {
        await _clientService.UpdateClientAsync(clientId, clientRequest, ct);
        return NoContent();
    }


    [HttpDelete("{clientId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int clientId, CancellationToken ct = default)
    {
        await _clientService.DeleteClientAsync(clientId, ct);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetClientByUserIdAsync([FromRoute] int userId, CancellationToken ct = default)
    {
        var client = await _clientService.GetClientByUserIdAsync(userId, ct);
        return Ok(client);
    }
}