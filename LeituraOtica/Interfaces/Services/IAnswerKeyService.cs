using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IAnswerKeyService
{
    public OperationResult SaveAnswerKey(AnswerKeyDto answerKey);
    public AnswerKeyDto? GetAnswerKey(int answerKeyId);
    public List<AnswerKeyDto>? GetAllAnswerKeys();
}