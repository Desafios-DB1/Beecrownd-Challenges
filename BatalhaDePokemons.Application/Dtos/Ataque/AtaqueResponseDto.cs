using BatalhaDePokemons.Domain.Enums;

namespace BatalhaDePokemons.Application.Dtos.Ataque;

public class AtaqueResponseDto
{
    public Guid AtaqueId { get; set; }
    public string Name { get; set; }
    public Tipo Tipo { get; set; }
    public int Poder { get; set; }
    public int PP { get; set; }

    public static implicit operator AtaqueResponseDto(Domain.Models.Ataque a)
    {
        return new AtaqueResponseDto()
        {
            AtaqueId = a.AtaqueId,
            Name = a.Name,
            Tipo = a.Tipo,
            Poder = a.Poder,
            PP = a.PP
        };
    }
}