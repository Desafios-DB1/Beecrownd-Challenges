using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;
using Bogus;

namespace BatalhaDePokemons.Test.Domain.Builders;

public class PokemonBuilder
{
    private Faker<Pokemon> _faker;
    public static PokemonBuilder Novo()
    {
        return new PokemonBuilder
        {
            _faker = new Faker<Pokemon>()
                .RuleFor(p=>p.PokemonId, f=>f.Random.Guid())
                .RuleFor(p => p.Nome, f => f.Name.FirstName())
                .RuleFor(p=>p.Level, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.IsDesmaiado, f => f.Random.Bool())
                .RuleFor(p => p.Tipo, f => f.Random.Enum<Tipo>())
                .RuleFor(p=>p.Status, _ => StatusCombateBuilder.Novo().Build())
        };
    }

    public Pokemon Build()
    {
        return _faker.Generate();
    }
}