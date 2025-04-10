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
                .RuleFor(p=>p.PontosDeVida, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Velocidade, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Defesa, f => f.Random.Int(1, 100))
                .RuleFor(p=>p.Ataque, f => f.Random.Int(1, 100))
        };
    }

    public StatusDeCombate Build()
    {
        return _faker.Generate();
    }
}