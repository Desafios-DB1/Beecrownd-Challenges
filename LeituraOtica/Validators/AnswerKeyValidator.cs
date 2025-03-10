using FluentValidation;
using LeituraOtica.Constants;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Repositories;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Validators;

public class AnswerKeyDtoValidator : AbstractValidator<AnswerKeyDto>
{
    public AnswerKeyDtoValidator(IExamRepository examRepository)
    {
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "ExamId"))
            .Must(examRepository.Exists).WithMessage(string.Format(ValidationMessages.NotExistError, "ExamId"));
        
        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Answers"));
    }
}