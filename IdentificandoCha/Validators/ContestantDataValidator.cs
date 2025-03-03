using FluentValidation;
using IdentificandoCha.DTOs;

namespace IdentificandoCha.Validators;

public class ContestantDataValidator : AbstractValidator<ContestantData>
{
    public ContestantDataValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Forneça um nome para o competidor!");
    }
}