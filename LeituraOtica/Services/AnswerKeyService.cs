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

    public AnswerKeyDto? GetAnswerKey(int answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId);
    }

    public List<AnswerKeyDto>? GetAllAnswerKeys()
    {
        return answerKeyRepository.GetAll();
    }

    public Dictionary<int, char>? GetAnswerKeyAnswers(int answerKeyId)
    {
        return answerKeyRepository.GetById(answerKeyId)?.Answers;
    }

    public int GetTotalQuestions(int answerKeyId)
    {
        var answerKeyAnswers = GetAnswerKeyAnswers(answerKeyId);
        return answerKeyAnswers?.Count ?? 0;
    }

    public bool AnswerKeyExists(int answerKeyId)
    {
        var answerKey = GetAnswerKey(answerKeyId);
        return answerKey != null;
    }
}