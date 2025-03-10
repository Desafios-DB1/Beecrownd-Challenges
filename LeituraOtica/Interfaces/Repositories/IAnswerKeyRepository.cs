using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IAnswerKeyRepository
{
    public AnswerKeyDto Save(AnswerKeyDto answerKey);
    public AnswerKeyDto? GetById(Guid answerKeyId);
    public List<AnswerKeyDto>? GetByExamId(Guid examId);
    public List<AnswerKeyDto>? GetAll();
    public AnswerKeyDto? GetByExamAndKeyId(Guid examId, Guid keyId);
    bool Exists(Guid answerKeyId);
}