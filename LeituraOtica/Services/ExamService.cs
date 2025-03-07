using FluentValidation;
using FluentValidation.Results;
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
        var validation = Validate(exam);
        
        if (!validation.IsValid)
        {
            return OperationResult.Failure(validation.Errors.ToString());
        }
        
        examRepository.Add(exam);
        return OperationResult.Success(exam);
    }

    public List<ExamDto> GetAllExams()
    {
        var exams = examRepository.GetAll();
        return exams ?? [];
    }

    public ExamDto? GetExam(int examId)
    {
        return examRepository.GetById(examId);
    }

    private ValidationResult Validate(ExamDto exam)
    {
        var validationResult = examValidator.Validate(exam);
        return validationResult.IsValid ? new ValidationResult() : validationResult;
    }
    
    public bool ExamExists(int examId)
    {
        var exam = GetExam(examId);
        return exam != null;
    }

    public double GetExamValue(int examId)
    {
        var exam = GetExam(examId);
        return exam?.Value ?? 0;
    }
}