using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class StudentAnswerRepository : IStudentAnswerRepository
{
    private static readonly List<StudentAnswerWithGradeDto> StudentAnswers = [];
    private int _currentId;
    
    public StudentAnswerWithGradeDto Add(StudentAnswerWithGradeDto answer)
    {
        answer.Id = ++_currentId;
        StudentAnswers.Add(answer);
        return answer;
    }

    public StudentAnswerWithGradeDto? GetById(int id)
    {
        return StudentAnswers.FirstOrDefault(x => x.Id == id);
    }

    public List<StudentAnswerWithGradeDto>? GetAll()
    {
        return StudentAnswers;
    }
}