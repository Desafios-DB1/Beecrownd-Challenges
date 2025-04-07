using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Domain.Models;

public class Pokemon
{
    public Pokemon()
    {
    }

    public Pokemon(string nome, int level, Tipo tipo, int hp, int spd, int def, int atk)
    {
        PokemonId = Guid.NewGuid();
        Nome = nome;
        Level = level;
        Tipo = tipo;
        Status = new StatusDeCombate(hp, spd, def, atk);
    }

    public Guid PokemonId { get; init; }
    public string Nome { get; set; }
    public int Level { get; set; }
    public bool IsDesmaiado { get; set; }
    public Tipo Tipo { get; set; }
    public StatusDeCombate Status { get; set; }
    public ICollection<Ataque> Ataques { get; set; }
    public ICollection<PokemonAtaque> PokemonAtaques { get; set; } = [];
}

