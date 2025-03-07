using FluentValidation;
using LeituraOtica.Constants;
using LeituraOtica.Dtos;
using LeituraOtica.Interfaces.Services;

namespace LeituraOtica.Validators;

public class AnswerKeyDtoValidator : AbstractValidator<AnswerKeyDto>
{
    public AnswerKeyDtoValidator(IExamService examService)
    {
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "ExamId"))
            .Must(examService.ExamExists).WithMessage(string.Format(ValidationMessages.NotExistError, "ExamId"));
        
        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "Answers"));
    }
}