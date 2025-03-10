using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IStudentAnswerRepository
{
    StudentAnswerWithGradeDto Add(StudentAnswerWithGradeDto answer);
    StudentAnswerWithGradeDto? GetById(Guid id);
    List<StudentAnswerWithGradeDto>? GetAll();
}