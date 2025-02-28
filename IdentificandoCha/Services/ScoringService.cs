namespace IdentificandoCha.Services;

public class ScoringService (ContestantService contestantService)
{
    public static void AddPoints(int contestantId, int points)
    {
        ContestantService.AddPoints(contestantId, points);
    }
}