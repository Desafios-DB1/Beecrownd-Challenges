using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class ExamCorrectionService(IAnswerKeyService answerKeyService, IExamService examService) : IExamCorrectionService
{
    public double Correction(StudentAnswerDto input)
    {
        var studentAnswers = input.ConvertedAnswers;
        var correctAnswers = answerKeyService.GetAnswerKey(input.AnswerKeyId)?.Answers;

        if (correctAnswers == null)
            return 0;

        foreach (var answer in correctAnswers.Where(answer => answer.Value == studentAnswers![answer.Key]))
        {
            input.Grade++;
        }
        
        var exam = examService.GetExam(input.ExamId);
        var examValue = exam!.Value;
        var totalQuestions = correctAnswers.Count;
        
        var questionValue = input.Grade / totalQuestions;
        var grade = questionValue * examValue;
        var roundedGrade = Math.Round(grade, 2);
        
        return roundedGrade;
    }
}