using LeituraOtica.Dtos;

namespace LeituraOtica.Interfaces.Repositories;

public interface IStudentAnswerRepository
{
    StudentAnswerDto Add(StudentAnswerDto answer);
    StudentAnswerDto? GetById(int id);
    List<StudentAnswerDto>? GetAll();
}