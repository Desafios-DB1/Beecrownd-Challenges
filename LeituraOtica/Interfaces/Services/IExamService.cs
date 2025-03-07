using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IExamService
{
    OperationResult AddExam(ExamDto exam);
    List<ExamDto>? GetAllExams();
    ExamDto? GetExam(int examId);
    bool ExamExists(int examId);
    double GetExamValue(int examId);
}