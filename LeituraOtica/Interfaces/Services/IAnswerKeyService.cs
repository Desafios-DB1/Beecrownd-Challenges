using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey);
    public AnswerKeyDto? GetAnswerKey(Guid answerKeyId);
    public List<AnswerKeyDto>? GetAllAnswerKeys();
    Dictionary<int, char>? GetAnswerKeyAnswers(Guid answerKeyId);
    int GetTotalQuestions(Guid answerKeyId);
}