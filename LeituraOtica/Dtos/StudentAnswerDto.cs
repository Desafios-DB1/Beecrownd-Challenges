namespace LeituraOtica.Dtos;

public class StudentAnswerDto (Guid examId, Guid answerKeyId, List<int[]> answers)
{
    public Guid ExamId { get; } = examId;
    public Guid AnswerKeyId { get; } = answerKeyId;
    public List<int[]> Answers { get; } = answers;

    public static implicit operator StudentAnswerDto(
        (Guid examId, Guid answerKeyId, List<int[]> answers) input)
    {
        return new StudentAnswerDto(input.examId, input.answerKeyId, input.answers);
    }
}