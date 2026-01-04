using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
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
    public async Task<IActionResult> GetClientsAsync()
    {
        var result = await _clientService.GetClientsAsync();
        return Ok(result);
    }

    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetClientByIdAsync([FromRoute]int clientId)
    {
        var result = await _clientService.GetClientByIdAsync(clientId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClientAsync([FromBody] ClientRequest client)
    {
        await _clientService.CreateClientAsync(client);
        return Created();
    }

    [HttpPut("{clientId}")]
    public async Task<IActionResult> UpdateClientAsync([FromRoute]int clientId, [FromBody]ClientRequest clientRequest)
    {
        await _clientService.UpdateClientAsync(clientId, clientRequest);
        return NoContent();
    }


    [HttpDelete("{clientId}")]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int clientId)
    {
        await _clientService.DeleteClientAsync(clientId);
        return NoContent();
    }
}