using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;

namespace LeituraOtica.Repositories;

public class StudentAnswerRepository : IStudentAnswerRepository
{
    private static readonly List<StudentAnswerWithGradeDto> StudentAnswers = [];
    
    public StudentAnswerWithGradeDto Add(StudentAnswerWithGradeDto answer)
    {
        answer.Id = Guid.NewGuid();
        StudentAnswers.Add(answer);
        return answer;
    }

    public StudentAnswerWithGradeDto? GetById(Guid id)
    {
        return StudentAnswers.FirstOrDefault(x => x.Id == id);
    }

    public List<StudentAnswerWithGradeDto>? GetAll()
    {
        return StudentAnswers;
    }
}