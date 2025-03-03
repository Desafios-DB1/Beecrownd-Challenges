using IdentificandoCha.DTOs;
using IdentificandoCha.Interfaces.Repository;
namespace IdentificandoCha.Repository;

public class ContestantRepository : IContestantRepository
{
    private static readonly List<ContestantData> Contestants = [];
    private static int _currentId;
    
    public ContestantData Add(ContestantData contestant)
    {
        contestant.Id= ++_currentId;
        Contestants.Add(contestant);
        return contestant;
    }
    
    public List<ContestantData> GetAll()
    {
        return Contestants.ToList();
    }

    public ContestantData GetById(int id)
    {
        return Contestants.FirstOrDefault(c => c.Id == id)!;
    }

    public void AddPoints(ContestantData contestant, int points)
    {
        contestant.Points += points;
    }

    public bool Exists(int id)
    {
        return Contestants.Any(c => c.Id == id);
    }
}