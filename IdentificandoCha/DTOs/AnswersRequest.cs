namespace IdentificandoCha.DTOs;

public class AnswersRequest
{
    public int ChallengeId { get; set; }
    public List<ContestantAnswer> Answers { get; set; }
}