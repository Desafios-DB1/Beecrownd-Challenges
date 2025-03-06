﻿namespace LeituraOtica.Dtos;

public class StudentAnswerDto (int studentId, int examId, int answerKeyId, List<int[]> answers)
{
    public int Id { get; set; }
    public int StudentId { get; } = studentId;
    public int ExamId { get; } = examId;
    public int AnswerKeyId { get; } = answerKeyId;
    public List<int[]> Answers { get; } = answers;
    public Dictionary<int, char>? ConvertedAnswers { get; set; }
    
    public double Grade { get; set; }

    public static implicit operator StudentAnswerDto(
        (int studentId, int examId, int answerKeyId, List<int[]> answers) input)
    {
        return new StudentAnswerDto(input.studentId, input.examId, input.answerKeyId, input.answers);
    }
}