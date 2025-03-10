namespace LeituraOtica.Dtos;

public class AnswerKeyDto(Guid examId, Dictionary<int, char>? answers)
{
    public Guid Id { get; set; }
    public Guid ExamId { get; } = examId;
    public Dictionary<int, char>? Answers { get; } = answers;

    public static implicit operator AnswerKeyDto((Guid examId, Dictionary<int, char>? answers) input)
    {
        return new AnswerKeyDto(input.examId, input.answers);
    }
}