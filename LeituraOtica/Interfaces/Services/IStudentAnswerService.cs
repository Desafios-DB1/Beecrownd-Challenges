using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IStudentAnswerService
{
    OperationResult AddStudentAnswer(StudentAnswerDto studentAnswer);
    StudentAnswerWithGradeDto? GetStudentAnswerById(Guid id);
    List<StudentAnswerWithGradeDto>? GetAllStudentsAnswers();
}