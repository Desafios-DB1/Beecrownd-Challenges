using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Domain.Models;

public class Ataque
{
    public Ataque() { }
    
    public Ataque(string nome, Tipo tipo, int poder, int precisao, int quantUsos)
    {
        Nome = nome;
        Tipo = tipo;
        Poder = poder;
        Precisao = precisao;
        QuantUsos = quantUsos;
    }
    public Guid AtaqueId { get; init; } = Guid.NewGuid();
    public string Nome { get; set; }
    public Tipo Tipo { get; set; }
    public int Poder { get; set; }
    public int Precisao { get; set; }
    public int QuantUsos { get; set; }
    public ICollection<Pokemon> Pokemons { get; init; }
    public ICollection<PokemonAtaque> PokemonAtaques { get; init; } = [];
    public DateTime DataHoraCriacao { get; init; } = DateTime.Now;
}