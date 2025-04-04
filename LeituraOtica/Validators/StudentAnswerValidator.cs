﻿using FluentValidation;
using LeituraOtica.Constants;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Validators;

public class StudentAnswerDtoValidator : AbstractValidator<StudentAnswerDto>
{
    public StudentAnswerDtoValidator(IExamRepository examRepository, IAnswerKeyRepository answerKeyRepository)
    {
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "ExamId"))
            .Must(examRepository.Exists).WithMessage(string.Format(ValidationMessages.NotExistError, "ExamID"));

        RuleFor(x => x.AnswerKeyId)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "AnswerKeyId"))
            .Must(answerKeyRepository.Exists).WithMessage(string.Format(ValidationMessages.NotExistError, "AnswerKeyId"));
        
        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Answers"));

        RuleForEach(x => x.Answers)
            .Must(answ => answ is { Length: 5 })
            .WithMessage("Cada resposta deve conter exatamente 5 valores!");
        
        RuleForEach(x => x.Answers)
            .Must(answ => answ.All(v => v is >= 0 and <= 255))
            .WithMessage("Cada resposta deve estar entre 0 e 255!");
    }
}