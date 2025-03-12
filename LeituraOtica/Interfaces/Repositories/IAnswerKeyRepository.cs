using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IAnswerKeyRepository
{
    public AnswerKeyDto Save(AnswerKeyDto answerKey);
    public AnswerKeyDto? GetById(Guid answerKeyId);
    public List<AnswerKeyDto>? GetAll();
    bool Exists(Guid answerKeyId);
}