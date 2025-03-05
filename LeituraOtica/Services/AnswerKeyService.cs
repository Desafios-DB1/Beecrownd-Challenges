using FluentValidation;
using FluentValidation.Results;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class AnswerKeyService(IAnswerKeyRepository answerKeyRepository,
    IValidator<AnswerKeyDto> answerKeyValidator) : IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey)
    {
        var validationResult = answerKeyValidator.Validate(answerKey);
        if (!validationResult.IsValid)
        { 
            return validationResult;
        }
        answerKeyRepository.Save(answerKey);
        return answerKey;
    }

    public AnswerKeyDto? GetAnswerKey(int examId, int answerKeyId)
    {
        return answerKeyRepository.GetByExamAndKeyId(examId, answerKeyId);
    }

    public List<AnswerKeyDto>? GetAllAnswerKeys()
    {
        return answerKeyRepository.GetAll();
    }
}