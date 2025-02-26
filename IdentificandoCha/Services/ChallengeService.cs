using IdentificandoCha.DTOs;

namespace IdentificandoCha.Services;

public class ChallengeService
{
    private readonly ContestantService _contestantService;

    private readonly Dictionary<int, int> _correctAnswers = new()
    {
        { 1, 3 },
        { 2, 4 },
        { 3, 1 },
        { 4, 2 },
        { 5, 5 },
    };

    public ChallengeService(ContestantService contestantService)
    {
        _contestantService = contestantService;
    }

    public void CheckAnswers(AnswersRequest request)
    {
        if (!_correctAnswers.ContainsKey(request.ChallengeId))
        {
            throw new Exception("Challenge not found");
        }
        
        var correctAnswer = _correctAnswers[request.ChallengeId];

        foreach (var contestantAnswer in request.Answers)
        {
            if (contestantAnswer.Answer == correctAnswer)
            {
                _contestantService.AddPoints(contestantAnswer.ContestantId, 100);
            }
        }
    }
}