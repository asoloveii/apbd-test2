using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apbd_test2.Models;

public class Track
{
    [Key]
    public int TrackId { get; set; }

    [Required] 
    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Precision(5, 2)]
    public decimal LengthInKm { get; set; }

    public ICollection<TrackRace> TrackRaces { get; set; } = new List<TrackRace>();
}