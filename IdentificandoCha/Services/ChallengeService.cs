using IdentificandoCha.DTOs;

namespace IdentificandoCha.Services;

public class ChallengeService(ContestantService contestantService)
{
    private readonly Dictionary<int, int> _correctAnswers = new()
    {
        { 1, 3 },
        { 2, 4 },
        { 3, 1 },
        { 4, 2 },
        { 5, 5 },
    };

    public void CheckAnswers(AnswersRequest request)
    {
        if (!_correctAnswers.TryGetValue(request.ChallengeId, out var correctAnswer))
        {
            throw new Exception("Challenge not found");
        }

        foreach (var contestant in request.Answers.Where(contestant => contestant.Answer == correctAnswer))
        {
            ContestantService.AddPoints(contestant.ContestantId, 100);
        }
    }
}