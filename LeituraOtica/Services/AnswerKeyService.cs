using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class AnswerKeyService(IAnswerKeyRepository answerKeyRepository,
    IValidator<AnswerKeyDto> answerKeyValidator,
    IExamService examService) : IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey)
    {
        var validationResult = answerKeyValidator.Validate(answerKey);
        if (!validationResult.IsValid)
        { 
            return OperationResult.Failure(validationResult.Errors.FirstOrDefault()?.ToString());
        }
        
        var exam = examService.GetExam(answerKey.ExamId);
        if (exam == null)
        {
            return OperationResult.Failure("A prova não existe!");
        }
        
        answerKeyRepository.Save(answerKey);
        return OperationResult.Success(answerKey);
    }

    public AnswerKeyDto? GetAnswerKey(int answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId);
    }

    public List<AnswerKeyDto>? GetAllAnswerKeys()
    {
        return answerKeyRepository.GetAll();
    }
}