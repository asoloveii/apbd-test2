namespace apbd_test2.DTOs;

public class GetRacersParticipationsDTO
{
    public int RacerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<PatricipationDTO> Participations { get; set; }
}

public class PatricipationDTO
{
    public RaceDTO Race { get; set; }
    public TrackDTO Track { get; set; }
    public int Laps { get; set; }
    public int FinishTimeInSeconds { get; set; }
    public int Position { get; set; }
}

public class RaceDTO
{
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
}

public class TrackDTO
{
    public string Name { get; set; }
    public decimal LengthInKm { get; set; }
}
