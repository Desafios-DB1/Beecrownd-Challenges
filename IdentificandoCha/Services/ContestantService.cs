using IdentificandoCha.DTOs;
using IdentificandoCha.Repository;

namespace IdentificandoCha.Services;

public class ContestantService ()
{
    public static ContestantData AddContestant(ContestantData contestant)
    {
        return ContestantRepository.Add(contestant);
    }
    
    public static List<ContestantData> GetAllContestants()
    {
        return ContestantRepository.GetAll();
    }

    public static void AddPoints(int contestantId, int points)
    {
        if (!ContestantRepository.Exists(contestantId)) return;
        
        var contestant = ContestantRepository.GetById(contestantId);
        ContestantRepository.AddPoints(contestant!, points);
    }
}