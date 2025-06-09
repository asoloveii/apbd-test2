using apbd_test2.Data;
using apbd_test2.DTOs;
using apbd_test2.Exceptions;
using apbd_test2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public async Task<GetRacersParticipationsDTO> GetRacersParticipationsAsync(int racerId)
    {
        var racer = await _context.Racers
            .Where(r => r.RacerId == racerId)
            .Select(r => new GetRacersParticipationsDTO()
            {
                RacerId = r.RacerId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Participations = r.RaceParticipations
                    .Select(rp => new PatricipationDTO()
                    {
                        Race = new RaceDTO()
                        {
                            Name = rp.TrackRace.Race.Name,
                            Location = rp.TrackRace.Race.Location,
                            Date = rp.TrackRace.Race.Date
                        },
                        Track = new TrackDTO()
                        {
                            Name = rp.TrackRace.Track.Name,
                            LengthInKm = rp.TrackRace.Track.LengthInKm
                        },
                        Laps = rp.TrackRace.Laps,
                        FinishTimeInSeconds = rp.FinishTimeInSeconds,
                        Position = rp.Position
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return racer;
    }

    public async Task AddRacersParticipationsAsync(AddRacersPatricipationDTO request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == request.RaceName);
            if (race is null)
            {
                throw new NotFoundException("Race not found");
            }
            
            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == request.TrackName);
            if (track is null)
            {
                throw new NotFoundException("Track not found");
            }
            
            var trackRace = await _context.TrackRaces
                .Include(tr => tr.RaceParticipations) 
                .FirstOrDefaultAsync(tr => tr.TrackId == track.TrackId && tr.RaceId == race.RaceId);
            
            if (trackRace == null)
            {
                trackRace = new TrackRace()
                {
                    TrackId = track.TrackId,
                    RaceId = race.RaceId,
                    Laps = 0, 
                    BestTimeInSeconds = null 
                };
                await _context.TrackRaces.AddAsync(trackRace);
                await _context.SaveChangesAsync(); 
            }
            
            foreach (var participationDto in request.Participations)
            {
                var racer = await _context.Racers.FindAsync(participationDto.RacerId);
                if (racer == null)
                {
                    throw new NotFoundException("Racer not found");
                }
                
                var existingParticipation = trackRace.RaceParticipations
                    .FirstOrDefault(rp => rp.RacerId == participationDto.RacerId);

                if (existingParticipation is null)
                {
                    // add new participation
                    var newParticipation = new RaceParticipation
                    {
                        TrackRaceId = trackRace.TrackRaceId,
                        RacerId = participationDto.RacerId,
                        FinishTimeInSeconds = participationDto.FinishTimeInSeconds,
                        Position = participationDto.Position
                    };
                    await _context.RaceParticipations.AddAsync(newParticipation);
                }
                else
                {
                    // update existing participation
                    existingParticipation.FinishTimeInSeconds = participationDto.FinishTimeInSeconds;
                    existingParticipation.Position = participationDto.Position;
                    _context.RaceParticipations.Update(existingParticipation);
                }

                // update BestTimeInSeconds for TrackRace if current finish time is shorter
                if (trackRace.BestTimeInSeconds == null || participationDto.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
                {
                    trackRace.BestTimeInSeconds = participationDto.FinishTimeInSeconds;
                    _context.TrackRaces.Update(trackRace);
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            transaction.RollbackAsync();
            throw;
        }
    }
    
}