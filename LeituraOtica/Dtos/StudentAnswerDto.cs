namespace LeituraOtica.Dtos;

public class StudentAnswerDto (int studentId, int examId, int answerKeyId, Dictionary<int, char> answers)
{
    public int Id { get; set; }
    public int StudentId { get; set; } = studentId;
    public int ExamId { get; set; } = examId;
    public int AnswerKeyId { get; set; } = answerKeyId;
    public Dictionary<int, char> Answers { get; set; } = answers;
    
    public double Grade { get; set; }

    public static implicit operator StudentAnswerDto(
        (int studentId, int examId, int answerKeyId, Dictionary<int, char> answers) input)
    {
        return new StudentAnswerDto(input.studentId, input.examId, input.answerKeyId, input.answers);
    }
}