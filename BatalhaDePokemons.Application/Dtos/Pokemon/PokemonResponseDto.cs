namespace BatalhaDePokemons.Application.Dtos.Pokemon;

public class PokemonResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public  int Level { get; set; }
    public  int Hp { get; set; }
    
    public static implicit operator PokemonResponseDto(Domain.Models.Pokemon pokemon)
    {
        return new PokemonResponseDto()
        {
            Id = pokemon.PokemonId,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Hp = pokemon.Status.Hp
        };
    }
}