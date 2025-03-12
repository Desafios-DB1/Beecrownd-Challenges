namespace LeituraOtica.Dtos;

public class StudentAnswerWithGradeDto (Guid examId, Guid answerKeyId, Dictionary<int, char> answers)
{
    public Guid Id { get; set; }
    public Guid ExamId { get; } = examId;
    public Guid AnswerKeyId { get; } = answerKeyId;
    public Dictionary<int, char>? Answers { get; } = answers;
    
    public double Grade { get; set; }
}