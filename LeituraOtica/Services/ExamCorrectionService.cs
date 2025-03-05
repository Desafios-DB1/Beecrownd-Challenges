using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Services;

public class ExamCorrectionService(IAnswerKeyService answerKeyService, IExamService examService) : IExamCorrectionService
{
    public double Correction(StudentAnswerDto input)
    {
        var studentAnswers = input.Answers;
        var correctAnswers = answerKeyService.GetAnswerKey(input.ExamId, input.AnswerKeyId)?.Answers;

        if (correctAnswers == null)
            return 0;

        foreach (var answer in correctAnswers)
        {
            if (studentAnswers.TryGetValue(answer.Key, out var studentAnswer) && studentAnswer == answer.Value)
                input.Grade++;
        }
        
        var exam = examService.GetExam(input.ExamId);
        var examValue = exam!.Value;
        var totalQuestions = correctAnswers.Count;
        
        return (input.Grade/totalQuestions)*examValue;
    }
}