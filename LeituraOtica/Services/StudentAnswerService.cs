using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class StudentAnswerService (IStudentAnswerRepository studentAnswerRepository, 
    IValidator<StudentAnswerDto> studentAnswerValidator,
    IExamCorrectionService examCorrectionService) : IStudentAnswerService
{
    public OperationResult AddStudentAnswer(StudentAnswerDto answer)
    {
        var validationResult = studentAnswerValidator.Validate(answer);
        if (!validationResult.IsValid)
        {
            return OperationResult.Failure(validationResult.Errors.FirstOrDefault()?.ToString());
        }
        
        answer.Grade = examCorrectionService.Correction(answer);
        
        studentAnswerRepository.Add(answer);
        return OperationResult.Success(answer);
    }

    public StudentAnswerDto? GetStudentAnswerById(int id)
    {
        return studentAnswerRepository.GetById(id);
    }

    public List<StudentAnswerDto>? GetAllStudentsAnswers()
    {
        return studentAnswerRepository.GetAll();
    }
}