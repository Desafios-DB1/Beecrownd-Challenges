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

    public BatalhaBuilder ComTurnos(int quant)
    {
        _faker.RuleFor(b => b.Turnos, (f, b) =>
        {
            var turnos = new List<Turno>();
            for (var i = 0; i < quant; i++)
            {
                turnos.Add(TurnoBuilder.Novo().ComBatalhaId(b.BatalhaId).Build());
            }
            return turnos;
        });
        return this;
    }
    
    public Batalha Build()
    {
        return _faker.Generate();
    }
}