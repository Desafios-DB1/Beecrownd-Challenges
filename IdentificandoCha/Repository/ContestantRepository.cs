using IdentificandoCha.DTOs;

namespace IdentificandoCha.Repository;

public class ContestantRepository
{
    private static readonly List<ContestantData> Contestants = [];
    private static int _currentId = 0;
    
    public static ContestantData Add(ContestantData contestant)
    {
        contestant.Id= ++_currentId;
        Contestants.Add(contestant);
        return contestant;
    }
    
    public static List<ContestantData> GetAll()
    {
        return Contestants.ToList();
    }

    public static ContestantData? GetById(int id)
    {
        return Contestants.FirstOrDefault(c => c.Id == id);
    }

    public static void AddPoints(ContestantData contestant, int points)
    {
        contestant.Points += points;
    }

    public static bool Exists(int id)
    {
        return Contestants.Any(c => c.Id == id);
    }
}