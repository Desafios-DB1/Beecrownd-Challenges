using IdentificandoCha.DTOs;

namespace IdentificandoCha.Interfaces.Services;

public interface IContestantService
{
    ContestantData AddContestant(ContestantData contestant);
    List<ContestantData> GetAllContestants();
    void AddPoints(int contestantId, int points);
}