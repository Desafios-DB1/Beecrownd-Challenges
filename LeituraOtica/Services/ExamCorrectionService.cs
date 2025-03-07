using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class ExamCorrectionService(IAnswerKeyService answerKeyService, IExamService examService) : IExamCorrectionService
{
    public double Correction(StudentAnswerDto input)
    {
        var studentAnswers = input.ConvertedAnswers;
        var correctAnswers = answerKeyService.GetAnswerKey(input.AnswerKeyId)?.Answers;

        if (correctAnswers == null || studentAnswers == null)
            return 0;

        var studentCorrectAnswers = correctAnswers.Count(answer => answer.Value == studentAnswers![answer.Key]);

        var exam = examService.GetExam(input.ExamId);
        var examValue = exam!.Value;
        var totalQuestions = correctAnswers.Count;
        
        var questionValue = examValue / totalQuestions;
        var grade = questionValue * studentCorrectAnswers;
        var roundedGrade = Math.Round(grade, 2);
        
        input.Grade = roundedGrade;
        return roundedGrade;
    }
}