using BatalhaDePokemons.Domain.Models;
using Bogus;

namespace BatalhaDePokemons.Test.Domain.Builders;

public class StatusCombateBuilder
{
    private Faker<StatusDeCombate> _faker;

    public static StatusCombateBuilder Novo()
    {
        return new StatusCombateBuilder
        {
            _faker = new Faker<StatusDeCombate>()
                .RuleFor(p=>p.Hp, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Spd, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Def, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Atk, f => f.Random.Int(1, 100))
        };
    }

    public StatusDeCombate Build()
    {
        return _faker.Generate();
    }
}