using FluentValidation;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;
using LeituraOtica.Responses;
using LeituraOtica.Validators;

namespace LeituraOtica.Services;

public class ValidationService(
    IValidator<AnswerKeyDto> answerKeyValidator,
    IValidator<ExamDto> examDtoValidator,
    IValidator<StudentAnswerDto> studentAnswerValidator) : IValidationService
{
    public OperationResult Validate(AnswerKeyDto answerKey)
    {
        var validationResult = answerKeyValidator.Validate(answerKey);
        return !validationResult.IsValid ? OperationResult.Failure(validationResult.Errors.ToString()) : OperationResult.Success(answerKey);
    }

    public OperationResult Validate(ExamDto exam)
    {
        var validationResult = examDtoValidator.Validate(exam);
        return !validationResult.IsValid ? OperationResult.Failure(validationResult.Errors.ToString()) : OperationResult.Success(exam);
    }
    
    public OperationResult Validate(StudentAnswerDto studentAnswer)
    {
        var validationResult = studentAnswerValidator.Validate(studentAnswer);
        return !validationResult.IsValid ? OperationResult.Failure(validationResult.Errors.ToString()) : OperationResult.Success(studentAnswer);
    }
    
    
}