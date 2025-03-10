using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class AnswerKeyRepository : IAnswerKeyRepository
{
    private static readonly List<AnswerKeyDto> AnswersKeys = [];
    
    public AnswerKeyDto Save(AnswerKeyDto answerKey)
    {
        answerKey.Id = Guid.NewGuid();
        AnswersKeys.Add(answerKey);
        return answerKey;
    }

    public AnswerKeyDto? GetById(Guid answerKeyId)
    {
        return AnswersKeys.FirstOrDefault(ak => ak.Id == answerKeyId);
    }
    public List<AnswerKeyDto> GetAll()
    {
        return AnswersKeys;
    }
    public bool Exists(Guid answerKeyId)
    {
        return AnswersKeys.Any(ak => ak.Id == answerKeyId);
    }
}