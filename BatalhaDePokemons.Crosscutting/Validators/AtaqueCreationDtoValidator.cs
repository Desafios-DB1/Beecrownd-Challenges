using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using FluentValidation;

namespace BatalhaDePokemons.Crosscutting.Validators;

public class AtaqueCreationDtoValidator : AbstractValidator<AtaqueCreationDto>
{
    public AtaqueCreationDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Caracteres.Duzentos)
            .WithMessage(ValidationErrors.TamanhoMaximo(Caracteres.Duzentos));

        RuleFor(x => x.Tipo)
            .Must(tipo => Enum.TryParse<Tipo>(tipo, true, out _))
            .WithMessage(ValidationErrors.EnumInvalido);
        
        RuleFor(x=>x.Poder)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));

        RuleFor(x => x.Precisao)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero))
            .LessThan(Caracteres.Cem).WithMessage(ValidationErrors.ValorMaximo(Caracteres.Cem));
        
        RuleFor(x => x.QuantUsos)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));
    }
}