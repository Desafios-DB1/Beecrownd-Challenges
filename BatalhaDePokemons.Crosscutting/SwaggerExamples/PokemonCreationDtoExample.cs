using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace BatalhaDePokemons.Crosscutting.SwaggerExamples;

public class PokemonCreationDtoExample : IExamplesProvider<PokemonCreationDto>
{
    public PokemonCreationDto GetExamples()
    {
        return new PokemonCreationDto
        {
            Nome = "Charmander",
            Level = 30,
            Tipo = Tipo.Fogo.ToString(),
            IsDesmaiado = false,
            Hp = 40,
            Spd = 20,
            Atk = 15,
            Def = 10
        };
    }
}