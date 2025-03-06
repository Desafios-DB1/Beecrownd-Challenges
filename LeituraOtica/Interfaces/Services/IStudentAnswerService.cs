using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IStudentAnswerService
{
    OperationResult AddStudentAnswer(StudentAnswerDto answer);
    StudentAnswerDto? GetStudentAnswerById(int id);
    List<StudentAnswerDto>? GetAllStudentsAnswers();
}