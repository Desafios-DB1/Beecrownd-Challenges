using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class ExamService(IExamRepository examRepository,
    IValidator<ExamDto> examValidator) : IExamService
{
    public OperationResult AddExam(ExamDto exam)
    {
        var validationResult = examValidator.Validate(exam);
        if (!validationResult.IsValid)
        {
            return OperationResult.Failure(validationResult.Errors.FirstOrDefault()?.ToString());
        }
        examRepository.Add(exam);
        return OperationResult.Success(exam);
    }

    public List<ExamDto>? GetAllExams()
    {
        return examRepository.GetAll();
    }

    public ExamDto? GetExam(int examId)
    {
        return examRepository.GetById(examId);
    }
}