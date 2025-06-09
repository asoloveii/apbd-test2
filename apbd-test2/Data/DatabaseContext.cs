using apbd_test2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Race> Races { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }
    public DbSet<Racer> Racers { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // seed some sample data
        modelBuilder.Entity<Racer>().HasData(new List<Racer>()
        {
            new Racer() { RacerId = 1, FirstName = "Racer1", LastName = "RacerLast" },
            new Racer() { RacerId = 2, FirstName = "Racer2", LastName = "RacerLast" },
            new Racer() { RacerId = 3, FirstName = "Racer3", LastName = "RacerLast" },
        });
        
        modelBuilder.Entity<Track>().HasData(new List<Track>()
        {
            new Track() { TrackId = 1, Name = "Track1", LengthInKm = 12 },
            new Track() { TrackId = 2, Name = "Track2", LengthInKm = 14 },
            new Track() { TrackId = 3, Name = "Track3", LengthInKm = 15 },
        });
        
        modelBuilder.Entity<Race>().HasData(new List<Race>()
        {
            new Race() { RaceId = 1, Name = "Race1", Date = new DateTime(1980, 8, 12), Location = "Location1"},
            new Race() { RaceId = 2, Name = "Race2", Date = new DateTime(1999, 5, 14), Location = "Location2"},
            new Race() { RaceId = 3, Name = "Race3", Date = new DateTime(2006, 1, 13), Location = "Location3"},
        });
        
        modelBuilder.Entity<TrackRace>().HasData(new List<TrackRace>()
        {
            new TrackRace() { RaceId = 1, TrackId = 1, Laps = 12, BestTimeInSeconds = 12 },
            new TrackRace() { RaceId = 2, TrackId = 2, Laps = 14, BestTimeInSeconds = 16 },
            new TrackRace() { RaceId = 3, TrackId = 3, Laps = 10, BestTimeInSeconds = 12 },
        });
        
        modelBuilder.Entity<RaceParticipation>().HasData(new List<RaceParticipation>()
        {
            new RaceParticipation() { RacerId = 1, TrackRaceId = 1, Position = 2, FinishTimeInSeconds = 13},
            new RaceParticipation() { RacerId = 2, TrackRaceId = 2, Position = 1, FinishTimeInSeconds = 16},
            new RaceParticipation() { RacerId = 3, TrackRaceId = 3, Position = 1, FinishTimeInSeconds = 12},
        });

    }

}