using BatalhaDePokemons.Domain.Enums;

namespace BatalhaDePokemons.Application.Dtos.Ataque;

public class AtaqueCreationDto
{
    public string Name { get; set; }
    public Tipo Tipo { get; set; }
    public int Poder { get; set; }
    public int Precisao { get; set; }
    public int PP { get; set; }

    public static implicit operator AtaqueCreationDto(Domain.Models.Ataque a)
    {
        return new AtaqueCreationDto()
        {
            Name = a.Name,
            Tipo = a.Tipo,
            Poder = a.Poder,
            Precisao = a.Precisao,
            PP = a.PP
        };
    }
}