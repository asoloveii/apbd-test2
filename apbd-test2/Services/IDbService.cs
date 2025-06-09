using apbd_test2.DTOs;

namespace apbd_test2.Services;

public interface IDbService
{
    Task<GetRacersParticipationsDTO> GetRacersParticipationsAsync(int racerId);
    Task AddRacersParticipationsAsync(AddRacersPatricipationDTO request);
}