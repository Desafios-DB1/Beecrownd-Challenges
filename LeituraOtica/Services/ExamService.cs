using FluentValidation;
using FluentValidation.Results;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class ExamService(IExamRepository examRepository, 
    IValidationService validationService) : IExamService
{
    public OperationResult AddExam(ExamDto exam)
    {
        var examValidation = validationService.Validate(exam);
        if (!examValidation.IsSuccess)
            return examValidation;
        
        examRepository.Add(exam);
        return OperationResult.Success(exam);
    }

    public List<ExamDto> GetAllExams()
    {
        var exams = examRepository.GetAll();
        return exams ?? [];
    }

    public ExamDto? GetExam(Guid examId)
    {
        return examRepository.GetById(examId);
    }
    
    public bool ExamExists(Guid examId)
    {
        var exam = GetExam(examId);
        return exam != null;
    }

    public double GetExamValue(Guid examId)
    {
        var exam = GetExam(examId);
        return exam?.Value ?? 0;
    }
}