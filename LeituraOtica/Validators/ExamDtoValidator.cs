using FluentValidation;
using LeituraOtica.Dtos;

namespace LeituraOtica.Validators;

public class ExamDtoValidator : AbstractValidator<ExamDto>
{
    public ExamDtoValidator()
    {
        RuleFor(x => x.SubjectName)
            .NotNull().WithMessage("O nome da matéria é obrigatório!")
            .NotEmpty().WithMessage("O nome da matéria não pode estar vazio!");
        
        RuleFor(x => x.Value)
            .Must(value => !double.IsNaN(value)).WithMessage("O valor da prova é obrigatório!")
            .GreaterThanOrEqualTo(0).WithMessage("A prova deve valer mais que 0!")
            .LessThanOrEqualTo(10).WithMessage("A prova deve valer 10 ou menos!");
    }
}