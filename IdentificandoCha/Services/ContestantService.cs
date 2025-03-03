using IdentificandoCha.DTOs;
using IdentificandoCha.Interfaces.Repository;
using IdentificandoCha.Interfaces.Services;
using IdentificandoCha.Repository;

namespace IdentificandoCha.Services;

public class ContestantService (IContestantRepository contestantRepository) : IContestantService
{
    public ContestantData AddContestant(ContestantData contestant)
    {
        return contestantRepository.Add(contestant);
    }
    
    public List<ContestantData> GetAllContestants()
    {
        return contestantRepository.GetAll();
    }

    public void AddPoints(int contestantId, int points)
    {
        if (!contestantRepository.Exists(contestantId)) return;
        
        var contestant = contestantRepository.GetById(contestantId);
        contestantRepository.AddPoints(contestant!, points);
    }
}