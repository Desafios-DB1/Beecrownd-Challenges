using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class StudentAnswerRepository : IStudentAnswerRepository
{
    private static readonly List<StudentAnswerDto> StudentAnswers = [];
    private static int _currentId;
    
    public StudentAnswerDto Add(StudentAnswerDto answer)
    {
        answer.Id = ++_currentId;
        StudentAnswers.Add(answer);
        return answer;
    }

    public StudentAnswerDto? GetById(int id)
    {
        return StudentAnswers.FirstOrDefault(x => x.Id == id);
    }

    public List<StudentAnswerDto>? GetAll()
    {
        return StudentAnswers;
    }
}