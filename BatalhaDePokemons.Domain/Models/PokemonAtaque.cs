namespace BatalhaDePokemons.Domain.Models;

public class PokemonAtaque
{
    public Guid PokemonId { get; init; }
    public Pokemon Pokemon { get; init; }
    
    public Guid AtaqueId { get; init; }
    public Ataque Ataque { get; init; }
}