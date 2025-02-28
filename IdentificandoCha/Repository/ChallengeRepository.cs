namespace IdentificandoCha.Repository;

public class ChallengeRepository
{
    private readonly Dictionary<int, int> _correctAnswers = new()
    {
        {1, 3},
        {2, 4},
        {3, 1},
        {4, 2},
        {5, 5}
    };
    
    public int? GetCorrectAnswer(int challengeId) =>
        _correctAnswers.TryGetValue(challengeId, out var correctAnswer) ? correctAnswer : null;
}