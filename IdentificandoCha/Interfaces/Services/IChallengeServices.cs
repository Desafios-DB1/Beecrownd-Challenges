using FluentValidation.Results;
using IdentificandoCha.DTOs;

namespace IdentificandoCha.Interfaces.Services;

public interface IChallengeServices
{
    void CheckAnswers(AnswersRequest request);
    ValidationResult ValidateAnswers(List<ContestantAnswer> answers);
}