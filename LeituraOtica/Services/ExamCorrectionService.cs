using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class ExamCorrectionService(IAnswerKeyService answerKeyService, IExamService examService) : IExamCorrectionService
{
    public double Correction(StudentAnswerDto input)
    {
        var examId = input.ExamId;
        var answerKeyId = input.AnswerKeyId;
        
        if (!examService.ExamExists(examId))
            return 0;
        
        var studentAnswers = input.ConvertedAnswers;
        var answerKeyAnswers = answerKeyService.GetAnswerKeyAnswers(answerKeyId);

        if (answerKeyAnswers == null || studentAnswers == null)
            return 0;

        var studentCorrectAnswers = answerKeyAnswers.Count(a =>
            studentAnswers.TryGetValue(a.Key, out var studentAnswer) &&
            studentAnswer == a.Value);

        var examValue = examService.GetExamValue(examId);
        var totalQuestions = answerKeyService.GetTotalQuestions(answerKeyId);
        var questionValue = CalcQuestionValue(totalQuestions, examValue);
        
        var grade = CalcRoundedGrade(studentCorrectAnswers, questionValue);
        
        return grade;
    }

    private static double CalcRoundedGrade(int correctAnswers, double questionValue)
    {
        var grade = correctAnswers * questionValue;
        return Math.Round(grade, 2);
    }

    private static double CalcQuestionValue(int totalQuestions, double examValue)
    {
        var questionValue = examValue / totalQuestions;
        return questionValue;
    }
    
}