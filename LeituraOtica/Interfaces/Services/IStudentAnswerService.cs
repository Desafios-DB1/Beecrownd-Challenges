using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IStudentAnswerService
{
    OperationResult AddStudentAnswer(StudentAnswerDto studentAnswer);
    StudentAnswerWithGradeDto? GetStudentAnswerById(int id);
    List<StudentAnswerWithGradeDto>? GetAllStudentsAnswers();
}