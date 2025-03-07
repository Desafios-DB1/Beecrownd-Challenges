using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class AnswerKeyRepository : IAnswerKeyRepository
{
    private static readonly List<AnswerKeyDto> AnswersKeys = [];
    private int _currentId;
    
    public AnswerKeyDto Save(AnswerKeyDto answerKey)
    {
        answerKey.Id = ++_currentId;
        AnswersKeys.Add(answerKey);
        return answerKey;
    }

    public AnswerKeyDto? GetById(int answerKeyId)
    {
        return AnswersKeys.FirstOrDefault(ak => ak.Id == answerKeyId);
    }

    public List<AnswerKeyDto>? GetByExamId(int examId)
    {
        return AnswersKeys.Where(x => x.ExamId == examId).ToList();
    }

    public List<AnswerKeyDto>? GetAll()
    {
        return AnswersKeys;
    }

    public AnswerKeyDto? GetByExamAndKeyId(int examId, int keyId)
    {
        return AnswersKeys.FirstOrDefault(ak => ak.Id == keyId && ak.ExamId == examId);
    }
}