using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class AnswerKeyService(IAnswerKeyRepository answerKeyRepository,
    IValidationService validationService) : IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey)
    {
        var answerKeyValidation = validationService.Validate(answerKey);
        if (!answerKeyValidation.IsSuccess)
            return answerKeyValidation;
        
        var newAnswerKey = answerKeyRepository.Save(answerKey);
        return OperationResult.Success(newAnswerKey);
    }

    public AnswerKeyDto? GetAnswerKey(Guid answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId);
    }

    public List<AnswerKeyDto> GetAllAnswerKeys()
    {
        return answerKeyRepository.GetAll() ?? [];
    }

    public Dictionary<int, char>? GetAnswerKeyAnswers(Guid answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId)?.Answers;
    }

    public int GetTotalQuestions(Guid answerKeyId)
    {
        var answerKeyAnswers = GetAnswerKeyAnswers(answerKeyId);
        return answerKeyAnswers?.Count ?? 0;
    }
}