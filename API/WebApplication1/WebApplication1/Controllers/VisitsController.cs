using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _visitService;

    public VisitsController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVisits()
    {
        var visits = await _visitService.GetAllVisits();
        return Ok(visits);
    }

    [HttpGet("{visitId}")]
    public async Task<IActionResult> GetVisitById([FromRoute]int visitId)
    {
        var visit = await _visitService.GetVisit(visitId);
        return Ok(visit);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetVisitByClientId([FromRoute]int clientId)
    {
        var visits = await _visitService.GetVisitsByClientId(clientId);
        return Ok(visits);
    }

    [HttpGet("barber/{barberId}")]
    public async Task<IActionResult> GetVisitsByBarberId([FromRoute]int barberId)
    {
        var visits = await _visitService.GetVisitsByBarberId(barberId);
        return Ok(visits);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVisit([FromBody]VisitRequest visitRequest)
    {
        var visit = await _visitService.CreateVisit(visitRequest);
        // return Created();
        return CreatedAtAction(
            nameof(GetVisitById),
            new {visitId = visit.VisitId},
            visit
        );

    }

    [HttpPut("{visitId}")]
    public async Task<IActionResult> UpdateVisit([FromRoute]int visitId, [FromBody]VisitRequest visitRequest)
    {
        await _visitService.UpdateVisit(visitId, visitRequest);
        return NoContent();
    }

    [HttpDelete("{visitId}")]
    public async Task<IActionResult> DeleteVisit([FromRoute]int visitId)
    {
        await _visitService.DeleteVisit(visitId);
        return NoContent();
    }
}