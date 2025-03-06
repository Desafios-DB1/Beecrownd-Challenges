using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;

namespace LeituraOtica.Services;

public class StudentAnswerService (IStudentAnswerRepository studentAnswerRepository, 
    IValidator<StudentAnswerDto> studentAnswerValidator,
    IExamCorrectionService examCorrectionService,
    IExamService examService,
    IAnswerKeyService answerKeyService) : IStudentAnswerService
{
    public OperationResult AddStudentAnswer(StudentAnswerDto answer)
    {
        var validationResult = studentAnswerValidator.Validate(answer);
        if (!validationResult.IsValid)
        {
            return OperationResult.Failure(validationResult.Errors.FirstOrDefault()?.ToString());
        }

        var exam = examService.GetExam(answer.ExamId);
        if (exam == null)
        {
            return OperationResult.Failure("Prova não encontrada!");
        }

        var answerKey = answerKeyService.GetAnswerKey(answer.AnswerKeyId);
        if (answerKey == null || answerKey.ExamId != answer.ExamId)
        {
            return OperationResult.Failure("Gabarito não encontrado!");
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