using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IAnswerKeyRepository
{
    public void Save(AnswerKeyDto answers);
    public AnswerKeyDto? GetById(int answerKeyId);
    public List<AnswerKeyDto>? GetByExamId(int examId);
    public List<AnswerKeyDto>? GetAll();
    public AnswerKeyDto? GetByExamAndKeyId(int examId, int keyId);
}