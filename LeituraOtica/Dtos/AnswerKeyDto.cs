﻿namespace LeituraOtica.Dtos;

public class AnswerKeyDto(int examId, Dictionary<int, char> answers)
{
    public int Id { get; set; }
    public int ExamId { get; } = examId;
    public Dictionary<int, char> Answers { get; } = answers;

    public static implicit operator AnswerKeyDto((int examId, Dictionary<int, char> answers) input)
    {
        return new AnswerKeyDto(input.examId, input.answers);
    }
}