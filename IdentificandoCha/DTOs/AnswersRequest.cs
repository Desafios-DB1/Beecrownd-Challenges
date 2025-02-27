namespace IdentificandoCha.DTOs;

public class AnswersRequest
{
    public int ChallengeId { get; init; }
    public required List<ContestantAnswer> Answers { get; init; }
}