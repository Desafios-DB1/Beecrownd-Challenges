using IdentificandoCha.DTOs;

namespace IdentificandoCha.Services;

public class ContestantService
{
    private static readonly List<ContestantData> Contestants = [];
    private static int _currentId = 0;

    public static ContestantData AddContestant(ContestantData contestant)
    {
        contestant.Id= ++_currentId;
        
        Contestants.Add(contestant);
        
        return contestant;
    }

    public static List<ContestantData>? GetAllContestants()
    {
        return Contestants.Count != 0 ? Contestants.ToList() : null;
    }

    public static void AddPoints(int contestantId, int points)
    {
        var contestant = Contestants.FirstOrDefault(c => c.Id == contestantId);
        if (contestant == null)
        {
            throw new Exception("Competidor não encontrado");
        }
        contestant.Points += points;
    }
}