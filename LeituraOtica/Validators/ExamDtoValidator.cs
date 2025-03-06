using FluentValidation;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class ExamDtoValidator : AbstractValidator<ExamDto>
{
    public ExamDtoValidator()
    {
        RuleFor(x => x.SubjectName)
            .NotEmpty().WithMessage("O nome da matéria não pode estar vazio!");

        RuleFor(x => x.ResponsibleName)
            .NotEmpty().WithMessage("O nome do responsável é obrigatório!");

        RuleFor(x => x.Value)
            .NotNull().WithMessage("O valor da prova é obrigatório!")
            .GreaterThanOrEqualTo(0).WithMessage("A prova deve valer mais que 0!");
    }
}