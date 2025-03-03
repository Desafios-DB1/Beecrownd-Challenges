using IdentificandoCha.DTOs;

namespace IdentificandoCha.Interfaces.Repository;

public interface IContestantRepository
{
    ContestantData Add(ContestantData contestant);
    List<ContestantData> GetAll();
    ContestantData GetById(int id);
    void AddPoints(ContestantData contestant, int points);
    bool Exists(int id);
}