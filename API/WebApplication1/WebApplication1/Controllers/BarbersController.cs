using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
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
    public async Task<IActionResult> GetBarbersAsync()
    {
        var barbers = await _barberService.GetBarbersAsync();
        return Ok(barbers);
    }

    [HttpGet("{barberId}")]
    public async Task<IActionResult> GetBarberByIdAsync([FromRoute]int barberId)
    {
        var barber = await _barberService.GetBarberAsync(barberId);
        return Ok(barber);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBarberAsync([FromBody]BarberRequest barberRequest)
    {
        await _barberService.CreateBarberAsync(barberRequest);
        return Created();
    }

    [HttpPut("{barberId}")]
    public async Task<IActionResult> UpdateBarberAsync([FromRoute]int barberId, [FromBody]BarberRequest barberRequest)
    {
        await _barberService.UpdateBarberAsync(barberId, barberRequest);
        return NoContent();
    }


    [HttpDelete("{barberId}")]
    public async Task<IActionResult> DeleteBarberAsync([FromRoute] int barberId)
    {
        await _barberService.DeleteBarberAsync(barberId);
        return NoContent();
    }
    
}