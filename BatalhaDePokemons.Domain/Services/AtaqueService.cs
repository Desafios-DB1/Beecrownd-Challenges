using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Domain.Services;

public class AtaqueService(IAtaqueRepository repository) : IAtaqueService
{
    public async Task<Guid> CriarAsync(AtaqueCreationDto ataque)
    {
        if (!Enum.TryParse<Tipo>(ataque.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException($"Tipo {ataque.Tipo} é inválido");
        
        var newAtaque = new Ataque(
            ataque.Nome, tipoConvertido, ataque.Poder, ataque.Precisao, ataque.QuantUsos);
        var newAtaqueId = await repository.AddAndCommitAsync(newAtaque);
        
        return newAtaqueId;
    }

    public async Task<AtaqueResponseDto?> ObterPorIdAsync(Guid id)
    {
        var ataque = await repository.FindByIdAsync(id) 
                     ?? throw new NotFoundException($"O ataque com ID {id} não foi encontrado.");
        
        return ataque.MapToResponseDto();
    }

    public async Task<List<AtaqueResponseDto>> ObterTodosAsync()
    {
        var ataques = await repository.FindAllAsync();
        return ataques.Select(ataque => ataque.MapToResponseDto()).ToList();
    }

    public async Task<List<AtaqueResponseDto>> ObterPorTipoAsync(Tipo tipo)
    {
        if (!Enum.TryParse<Tipo>(tipo.ToString(), true, out var tipoConvertido))
            throw new InvalidArgumentException($"Tipo {tipo} é invalido!");
        
        var ataques = await repository.FindByTipoAsync(tipoConvertido);
        return ataques.Select(ataque => ataque.MapToResponseDto()).ToList();
    }

    public async Task<AtaqueResponseDto> AtualizarAsync(Guid ataqueId, AtaqueCreationDto ataque)
    {
        if (!Enum.TryParse<Tipo>(ataque.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException($"Tipo {ataque.Tipo} é inválido");
        
        var ataqueAntigo = await repository.FindByIdAsync(ataqueId)
            ?? throw new NotFoundException($"O ataque com ID {ataqueId} não foi encontrado.");
        
        ataqueAntigo.Nome = ataque.Nome;
        ataqueAntigo.Tipo = tipoConvertido;
        ataqueAntigo.Poder = ataque.Poder;
        ataqueAntigo.Precisao = ataque.Precisao;
        ataqueAntigo.QuantUsos = ataque.QuantUsos;
        
        var ataqueAtualizado = await repository.UpdateAndCommitAsync(ataqueAntigo);
        return ataqueAtualizado.MapToResponseDto();
    }

    public async Task RemoverAsync(Guid id)
    {
        var ataque = await repository.FindByIdAsync(id) 
                     ?? throw new NotFoundException($"O ataque com ID {id} não foi encontrado.");
        await repository.RemoveAndCommitAsync(ataque);
    }
}