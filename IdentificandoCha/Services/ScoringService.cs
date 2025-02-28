using IdentificandoCha.Interfaces.Services;

namespace IdentificandoCha.Services;

public class ScoringService (IContestantService contestantService) : IScoringService
{
    public void AddPoints(int contestantId, int points)
    {
        contestantService.AddPoints(contestantId, points);
    }
}