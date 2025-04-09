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
            PontosDeVida = 40,
            Velocidade = 20,
            Ataque = 15,
            Defesa = 10
        };
    }
}