using apbd_test2.DTOs;
using apbd_test2.Exceptions;
using apbd_test2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackRacesController : ControllerBase
{
    private readonly IDbService _dbService;

    public TrackRacesController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("participants")]
    public async Task<IActionResult> AddRacersParticipation(AddRacersPatricipationDTO request)
    {
        try
        {
            await _dbService.AddRacersParticipationsAsync(request);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}