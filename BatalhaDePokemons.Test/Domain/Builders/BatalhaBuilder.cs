using BatalhaDePokemons.Domain.Models;
using Bogus;

namespace BatalhaDePokemons.Test.Domain.Builders;

public class BatalhaBuilder
{
    private Faker<Batalha> _faker;

    public static BatalhaBuilder Novo()
    {
        return new BatalhaBuilder
        {
            _faker = new Faker<Batalha>()
                .RuleFor(b => b.BatalhaId, faker => faker.Random.Guid())
                .RuleFor(b => b.Pokemon1Id, faker => faker.Random.Guid())
                .RuleFor(b => b.Pokemon2Id, faker => faker.Random.Guid())
                .RuleFor(b => b.VencedorId, Guid.Empty)
                .RuleFor(b => b.IsFinalizada, faker => faker.Random.Bool())
                .RuleFor(b => b.ProximoTurnoDoPokemonId, Guid.Empty)
        };
    }

    public BatalhaBuilder ComTurnos(int quantidade)
    {
        _faker.RuleFor(b => b.Turnos, TurnoBuilder.Novo().BuildMany(quantidade));
        return this;
    }

    public BatalhaBuilder ComPokemon(Guid pokemonId)
    {
        _faker.RuleFor(b => b.Pokemon1Id, pokemonId);
        return this;
    }

    public BatalhaBuilder NaoFinalizada()
    {
        _faker.RuleFor(b => b.IsFinalizada, false);
        return this;
    }

    public BatalhaBuilder ComAtacante(Guid atacanteId)
    {
        _faker.RuleFor(b=>b.ProximoTurnoDoPokemonId, atacanteId);
        return this;
    }
    
    public Batalha Build()
    {
        return _faker.Generate();
    }
}