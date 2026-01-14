using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetVisits(
        int page = 1,
        int pageSize = 5,
        CancellationToken ct = default)
    {
        var visits = await _visitService.GetAllVisits(page, pageSize, ct);
        return Ok(visits);
    }

    [HttpGet("{visitId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetVisitById([FromRoute]int visitId, CancellationToken ct = default)
    {
        var visit = await _visitService.GetVisit(visitId, ct);
        return Ok(visit);
    }

    [HttpGet("user/{visitId}/{userId}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetVisitByUserId([FromRoute]int visitId, [FromRoute]int userId, CancellationToken ct = default)
    {
        var visit = await _visitService.GetVisitByUserId(visitId, userId, ct);
        return Ok(visit);
    }

    [HttpGet("client/{clientId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetVisitsByClientId([FromRoute]int clientId, CancellationToken ct = default)
    {
        var visits = await _visitService.GetVisitsByClientId(clientId, ct);
        return Ok(visits);
    }

    [HttpGet("barber/{barberId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetVisitsByBarberId([FromRoute]int barberId, CancellationToken ct = default)
    {
        var visits = await _visitService.GetVisitsByBarberId(barberId, ct);
        return Ok(visits);
    }


    [Authorize(Roles = "admin,user")]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetVisitsByUserId(
        [FromRoute] int userId, 
        int page = 1, 
        int pageSize = 5, 
        CancellationToken ct = default)
    {
        var visits = await _visitService.GetVisitsByUserId(userId, page, pageSize, ct);
        return Ok(visits);
    }

    [HttpPost]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> CreateVisit([FromBody]VisitRequest visitRequest, CancellationToken ct = default)
    {
        var visit = await _visitService.CreateVisit(visitRequest, ct);
        return CreatedAtAction(
            nameof(GetVisitById),
            new {visitId = visit.VisitId},
            visit
        );

    }

    [HttpPut("{visitId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> UpdateVisit([FromRoute]int visitId, [FromBody]VisitRequest visitRequest, CancellationToken ct = default)
    {
        await _visitService.UpdateVisit(visitId, visitRequest, ct);
        return NoContent();
    }

    [HttpDelete("{visitId}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> DeleteVisit([FromRoute]int visitId, CancellationToken ct = default)
    {
        await _visitService.DeleteVisit(visitId, ct);
        return NoContent();
    }
}