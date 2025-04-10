using BatalhaDePokemons.Domain.Models;
using Bogus;

namespace BatalhaDePokemons.Test.Domain.Builders;

public class TurnoBuilder
{
    private Faker<Turno> _faker;

    public static TurnoBuilder Novo()
    {
        return new TurnoBuilder
        {
            _faker = new Faker<Turno>()
                .RuleFor(t=>t.TurnoId, f=>f.Random.Guid())
                .RuleFor(t=>t.BatalhaId, f=>f.Random.Guid())
                .RuleFor(t=>t.NumeroTurno, f=>f.Random.Int())
                .RuleFor(t=>t.AtacanteId, f=>f.Random.Guid())
                .RuleFor(t=>t.AlvoId, f=>f.Random.Guid())
                .RuleFor(t=>t.AtaqueUtilizadoId, f=>f.Random.Guid())
                .RuleFor(t=>t.DanoCausado, f=>f.Random.Int())
        };
    }

    public TurnoBuilder ComBatalhaId(Guid batalhaId)
    {
        _faker.RuleFor(t=>t.BatalhaId, batalhaId);
        return this;
    }

    public Turno Build()
    {
        return _faker.Generate();
    }

    public List<Turno> BuildMany(int quantidade)
    {
        return _faker.Generate(quantidade);
    }
}