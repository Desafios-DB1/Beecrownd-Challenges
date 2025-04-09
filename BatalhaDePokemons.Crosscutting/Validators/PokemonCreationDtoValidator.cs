using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using FluentValidation;

namespace BatalhaDePokemons.Crosscutting.Validators;

public class PokemonCreationDtoValidator : AbstractValidator<PokemonCreationDto>
{
    public PokemonCreationDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Caracteres.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo(Caracteres.Duzentos));

        RuleFor(x => x.Level)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero))
            .LessThan(Caracteres.Cem).WithMessage(ValidationErrors.ValorMaximo(Caracteres.Cem));

        RuleFor(x => x.Tipo)
            .Must(tipo => Enum.TryParse<Tipo>(tipo, true, out _))
            .WithMessage(ValidationErrors.EnumInvalido);

        RuleFor(x => x.PontosDeVida)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));
        
        RuleFor(x=>x.Ataque)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));
        
        RuleFor(x=>x.Defesa)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));
        
        RuleFor(x=>x.Defesa)
            .GreaterThan(Caracteres.Zero).WithMessage(ValidationErrors.ValorMinimo(Caracteres.Zero));
    }
}