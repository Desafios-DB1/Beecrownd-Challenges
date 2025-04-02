using BatalhaDePokemons.Domain.Enums;

namespace BatalhaDePokemons.Domain.Models;

public class Ataque
{
    public Ataque() { }
    
    public Ataque(string nome, Tipo tipo, int poder, int precisao, int pp)
    {
        AtaqueId = Guid.NewGuid();
        Name = nome;
        Tipo = tipo;
        Poder = poder;
        Precisao = precisao;
        PP = pp;
    }
    public Guid AtaqueId { get; set; }
    public string Name { get; set; }
    public Tipo Tipo { get; set; }
    public int Poder { get; set; }
    public int Precisao { get; set; }
    public int PP { get; set; }
    public ICollection<Pokemon> Pokemons { get; set; }
    public ICollection<PokemonAtaque> PokemonAtaques { get; set; } = [];

}