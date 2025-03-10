using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class AnswerKeyService(IAnswerKeyRepository answerKeyRepository,
    IValidationService validationService,
    IExamService examService) : IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey)
    {
        var answerKeyValidation = validationService.Validate(answerKey);
        if (!answerKeyValidation.IsSuccess)
            return answerKeyValidation;
        
        answerKeyRepository.Save(answerKey);
        return OperationResult.Success(answerKey);
    }

    public AnswerKeyDto? GetAnswerKey(Guid answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId);
    }

    public List<AnswerKeyDto>? GetAllAnswerKeys()
    {
        return answerKeyRepository.GetAll();
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

    public bool AnswerKeyExists(Guid answerKeyId)
    {
        var answerKey = GetAnswerKey(answerKeyId);
        return answerKey != null;
    }
}