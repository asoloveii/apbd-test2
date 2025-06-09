using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Models;

[Table("Race_Participation")]
[PrimaryKey(nameof(RacerId), nameof(TrackRaceId))]
public class RaceParticipation
{
    [ForeignKey(nameof(TrackRace))]
    public int TrackRaceId { get; set; }
    
    [ForeignKey(nameof(Racer))]
    public int RacerId { get; set; }
    
    public TrackRace TrackRace { get; set; } = null!;
    public Racer Racer { get; set; } = null!; 

    [Required]
    public int FinishTimeInSeconds { get; set; }

    [Required]
    public int Position { get; set; }
    
}