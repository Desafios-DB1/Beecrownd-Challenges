using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;
using Bogus;

namespace BatalhaDePokemons.Test.Domain.Builders;

public class AtaqueBuilder
{
    private Faker<Ataque> _faker;

    public static AtaqueBuilder Novo()
    {
        return new AtaqueBuilder()
        {
            _faker = new Faker<Ataque>()
                .RuleFor(a => a.AtaqueId, faker => faker.Random.Guid())
                .RuleFor(a => a.Nome, f => f.Lorem.Word())
                .RuleFor(a => a.Tipo, f => f.Random.Enum<Tipo>())
                .RuleFor(a => a.Poder, f => f.Random.Int())
                .RuleFor(a => a.Precisao, f => f.Random.Int())
                .RuleFor(a => a.QuantUsos, f => f.Random.Int())
        };
    }

    public AtaqueBuilder ComTipo(Tipo tipo)
    {
        _faker.RuleFor(a => a.Tipo, tipo);
        return this;
    }

    public Ataque Build()
        => _faker.Generate();
}