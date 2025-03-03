namespace IdentificandoCha.Interfaces.Repository;

public interface IChallengeRepository
{
    int? GetCorrectAnswer(int challengeId);
}