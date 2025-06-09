using apbd_test2.Exceptions;
using apbd_test2.Models;
using apbd_test2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacersController : ControllerBase
{
    private readonly IDbService _dbService;

    public RacersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{racerId}/participations")]
    public async Task<IActionResult> GetRacersParticipation(int racerId)
    {
        try
        {
            var order = await _dbService.GetRacersParticipationsAsync(racerId);
            return Ok(order);
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }
}
