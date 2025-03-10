using LeituraOtica.Dtos;
using LeituraOtica.Responses;

namespace LeituraOtica.Interfaces.Services;

public interface IValidationService
{ 
    OperationResult Validate(AnswerKeyDto answerKey);
    OperationResult Validate(ExamDto exam);
    OperationResult Validate(StudentAnswerDto studentAnswer);
}