using System.ComponentModel.DataAnnotations;

namespace apbd_test2.DTOs;

public class AddRacersPatricipationDTO
{
    [Required]
    [MaxLength(50)]
    public string RaceName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string TrackName { get; set; } = string.Empty;

    [Required]
    public List<RacersPatricipationDTO> Participations { get; set; } = new List<RacersPatricipationDTO>();
}

public class RacersPatricipationDTO
{
    [Required]
    public int RacerId { get; set; }

    [Required]
    public int Position { get; set; }

    [Required]
    public int FinishTimeInSeconds { get; set; }
}