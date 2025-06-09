using System.ComponentModel.DataAnnotations;

namespace apbd_test2.Models;

public class Race
{
    [Key]
    public int RaceId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    public ICollection<TrackRace> TrackRaces { get; set; } = new List<TrackRace>();
}