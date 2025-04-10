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
                .RuleFor(p=>p.Nivel, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.IsDesmaiado, f => f.Random.Bool())
                .RuleFor(p => p.Tipo, f => f.Random.Enum<Tipo>())
                .RuleFor(p=>p.Status, _ => StatusCombateBuilder.Novo().Build())
        };
    }

    public PokemonBuilder ComDesmaiado()
    {
        _faker.RuleFor(p => p.IsDesmaiado, true);
        return this;
    }

    public PokemonBuilder NaoDesmaiado()
    {
        _faker.RuleFor(p => p.IsDesmaiado, false);
        return this;
    }

    public PokemonBuilder ComAtaques()
    {
        _faker.RuleFor(p => p.Ataques, f =>
        {
            var ataques = new List<Ataque> { AtaqueBuilder.Novo().Build(), AtaqueBuilder.Novo().Build() };
            return ataques;
        });
        return this;
    }
        
    public Pokemon Build()
    {
        return _faker.Generate();
    }
}