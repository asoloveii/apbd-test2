using System.ComponentModel.DataAnnotations;

namespace apbd_test2.Models;


public class Racer
{
    [Key]
    public int RacerId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<RaceParticipation> RaceParticipations { get; set; } = new List<RaceParticipation>();
}