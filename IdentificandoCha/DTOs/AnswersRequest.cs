namespace IdentificandoCha.DTOs;

public class AnswersRequest
{
    public int ChallengeId { get; private init; }
    public required List<ContestantAnswer> Answers { get; init; }

    public static implicit operator AnswersRequest((int challengeId, List<ContestantAnswer> answers) data)
    {
        return new AnswersRequest()
        {
            ChallengeId = data.challengeId,
            Answers = data.answers
        };
    }
}