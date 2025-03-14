﻿using FluentValidation;
using FluentValidation.Results;
using IdentificandoCha.DTOs;
using IdentificandoCha.Exceptions;
using IdentificandoCha.Interfaces.Repository;
using IdentificandoCha.Interfaces.Services;
using IdentificandoCha.Repository;

namespace IdentificandoCha.Services;

public class ChallengeService(
    IValidator<List<ContestantAnswer>> answerValidator,
    IChallengeRepository challengeRepository,
    IScoringService scoringService)
    : IChallengeServices
{
    public void CheckAnswers(AnswersRequest request)
    {
        var correctAnswer = challengeRepository.GetCorrectAnswer(request.ChallengeId)
                            ?? throw new BusinessException("Desafio não encontrado!");

        foreach (var contestant in request.Answers.Where(c => c.Answer == correctAnswer))
        {
            scoringService.AddPoints(contestant.ContestantId, 100);
        }
    }

    public ValidationResult ValidateAnswers(List<ContestantAnswer> answers)
    {
        var validationResult = answerValidator.Validate(answers);
        return !validationResult.IsValid ? validationResult : new ValidationResult();
    }
}