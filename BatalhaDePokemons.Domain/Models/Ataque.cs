using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;

namespace BatalhaDePokemons.Domain.Models;

public class Ataque
{
    public Ataque() { }
    
    public Ataque(string nome, Tipo tipo, int poder, int precisao, int quantUsos)
    {
        AtaqueId = Guid.NewGuid();
        Nome = nome;
        Tipo = tipo;
        Poder = poder;
        Precisao = precisao;
        QuantUsos = quantUsos;
    }
    public Guid AtaqueId { get; set; }
    public string Nome { get; set; }
    public Tipo Tipo { get; set; }
    public int Poder { get; set; }
    public int Precisao { get; set; }
    public int QuantUsos { get; set; }
    public ICollection<Pokemon> Pokemons { get; set; }
    public ICollection<PokemonAtaque> PokemonAtaques { get; set; } = [];

    public AtaqueResponseDto MapToResponseDto()
    {
        return new AtaqueResponseDto()
        {
            Nome = Nome,
            Tipo = Tipo,
            Poder = Poder,
            QuantUsos = QuantUsos,
        };
    }

    public AtaqueCreationDto MapToCreationDto()
    {
        return new AtaqueCreationDto()
        {
            Nome = Nome,
            Tipo = Tipo.ToString(),
            Poder = Poder,
            Precisao = Precisao,
            QuantUsos = QuantUsos,
        };
    }
}