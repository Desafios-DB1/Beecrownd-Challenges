using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace BatalhaDePokemons.Crosscutting.SwaggerExamples;

public class AtaqueCreationDtoExample : IExamplesProvider<AtaqueCreationDto>
{
    public AtaqueCreationDto GetExamples()
    {
        return new AtaqueCreationDto
        {
            Nome = "Faísca",
            Tipo = Tipo.Fogo.ToString(),
            Poder = 15,
            Precisao = 70,
            QuantUsos = 10
        };
    }
}