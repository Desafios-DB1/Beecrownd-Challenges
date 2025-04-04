namespace BatalhaDePokemons.Domain.Models;

public class PokemonAtaque
{
    public Guid PokemonId { get; set; }
    public Pokemon Pokemon { get; set; }
    
    public Guid AtaqueId { get; set; }
    public Ataque Ataque { get; set; }
}