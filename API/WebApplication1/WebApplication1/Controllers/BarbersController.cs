using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarbersController : ControllerBase
{
    private readonly IBarberService _barberService;

    public BarbersController(IBarberService barberService)
    {
        _barberService = barberService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetBarbersAsync(
        int page = 1, 
        int pageSize = 5,
        CancellationToken ct = default)
    {
        var barbers = await _barberService.GetBarbersAsync(page, pageSize, ct);
        return Ok(barbers);
    }

    [HttpGet("{barberId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetBarberByIdAsync([FromRoute]int barberId, CancellationToken ct = default)
    {
        var barber = await _barberService.GetBarberAsync(barberId, ct);
        return Ok(barber);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateBarberAsync([FromBody]BarberRequest barberRequest, CancellationToken ct = default)
    {
        await _barberService.CreateBarberAsync(barberRequest, ct);
        return Created();
    }

    [HttpPut("{barberId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateBarberAsync([FromRoute]int barberId, [FromBody]BarberRequest barberRequest, CancellationToken ct = default)
    {
        await _barberService.UpdateBarberAsync(barberId, barberRequest, ct);
        return NoContent();
    }


    [HttpDelete("{barberId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteBarberAsync([FromRoute] int barberId, CancellationToken ct = default)
    {
        await _barberService.DeleteBarberAsync(barberId, ct);
        return NoContent();
    }
    
}