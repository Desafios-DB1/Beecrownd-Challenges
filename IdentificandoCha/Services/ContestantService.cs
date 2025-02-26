using IdentificandoCha.DTOs;

namespace IdentificandoCha.Services;

public class ContestantService
{
    private static List<ContestantData> _contestants = new List<ContestantData>();
    private static int _currentId = 0;

    public ContestantData AddContestant(ContestantData contestant)
    {
        contestant.Id= ++_currentId;
        
        _contestants.Add(contestant);
        
        return contestant;
    }

    public List<ContestantData> GetAllContestants()
    {
        return _contestants.ToList();
    }

    public ContestantData GetContestantById(int id)
    {
        return _contestants.FirstOrDefault(x => x.Id == id);
    }
}