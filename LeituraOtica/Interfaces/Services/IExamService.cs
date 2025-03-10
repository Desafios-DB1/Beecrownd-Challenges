using FluentValidation.Results;
using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IExamService
{
    OperationResult AddExam(ExamDto exam);
    List<ExamDto> GetAllExams();
    ExamDto? GetExam(Guid examId);
    bool ExamExists(Guid examId);
    double GetExamValue(Guid examId);
}