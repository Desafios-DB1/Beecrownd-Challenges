using FluentValidation.Results;
using LeituraOtica.Dtos;

namespace LeituraOtica.Responses;

public class OperationResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public ValidationResult? ValidationErrors { get; set; }
    
    public OperationResult(){}

    private OperationResult(object data)
    {
        Success = true;
        Data = data;
        Message = "Adicionado com sucesso!";
    }

    private OperationResult(ValidationResult validationResult)
    {
        Success = validationResult.IsValid;
        Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage;
        ValidationErrors = validationResult;
    }

    public static implicit operator OperationResult(ExamDto exam)
    {
        return new OperationResult(exam);
    }

    public static implicit operator OperationResult(ValidationResult validationResult)
    {
        return new OperationResult(validationResult);
    }

    public static implicit operator OperationResult(AnswerKeyDto answerKey)
    {
        return new OperationResult(answerKey);
    }

    public static implicit operator OperationResult(StudentAnswerDto studentAnswer)
    {
        return new OperationResult(studentAnswer);
    }
}