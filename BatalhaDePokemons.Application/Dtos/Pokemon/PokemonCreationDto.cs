using BatalhaDePokemons.Domain.Enums;

namespace BatalhaDePokemons.Application.Dtos.Pokemon;

public class PokemonCreationDto
{
    public required string Name { get; set; }
    public int Level { get; set; }
    public Tipo Tipo { get; set; }
    public int Hp { get; set; }
    public int Spd { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    
    public static implicit operator PokemonCreationDto(Domain.Models.Pokemon pokemon)
    {
        return new PokemonCreationDto()
        {
            Name = pokemon.Name,
            Level = pokemon.Level,
            Tipo = pokemon.Tipo,
            Hp = pokemon.Status.Hp,
            Spd = pokemon.Status.Spd,
            Atk = pokemon.Status.Atk,
            Def = pokemon.Status.Def,
        };
    }
}