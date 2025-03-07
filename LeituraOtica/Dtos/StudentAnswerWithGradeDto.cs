namespace LeituraOtica.Dtos;

public class StudentAnswerWithGradeDto (int studentId, int examId, int answerKeyId, Dictionary<int, char> answers)
{
    public int Id { get; set; }
    public int StudentId { get; } = studentId;
    public int ExamId { get; } = examId;
    public int AnswerKeyId { get; } = answerKeyId;
    public Dictionary<int, char>? Answers { get; set; } = answers;
    
    public double Grade { get; set; }
}