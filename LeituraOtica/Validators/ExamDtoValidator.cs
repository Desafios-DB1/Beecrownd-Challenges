using FluentValidation;
using LeituraOtica.Constants;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class ExamDtoValidator : AbstractValidator<ExamDto>
{
    public ExamDtoValidator()
    {
        RuleFor(x => x.SubjectName)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "SubjectName"));

        RuleFor(x => x.ResponsibleName)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.RequiredField, "ResponsibleName"));

        RuleFor(x => x.Value)
            .NotNull().WithMessage(string.Format(ValidationMessages.RequiredField, "Value"))
            .GreaterThanOrEqualTo(0).WithMessage("A prova deve valer mais que 0!");
    }
}