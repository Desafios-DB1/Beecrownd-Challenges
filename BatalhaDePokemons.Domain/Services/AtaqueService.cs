using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Mappers;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Domain.Services;

public class AtaqueService(IAtaqueRepository repository) : IAtaqueService
{
    public async Task<Guid> CriarAsync(AtaqueCreationDto ataque)
    {
        if (!Enum.TryParse<Tipo>(ataque.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException(ExceptionMessages.TipoInvalido(ataque.Tipo));
        
        var newAtaque = new Ataque(
            ataque.Nome, tipoConvertido, ataque.Poder, ataque.Precisao, ataque.QuantUsos);
        var newAtaqueId = await repository.AdicionarESalvarAsync(newAtaque);
        
        return newAtaqueId;
    }

    public async Task<AtaqueResponseDto?> ObterPorIdAsync(Guid id)
    {
        var ataque = await repository.ObterPorIdAsync(id) 
                     ?? throw new NotFoundException(ExceptionMessages.AtaqueNaoEncontrado(id));
        
        return AtaqueMapper.MapToResponseDto(ataque);
    }

    public async Task<List<AtaqueResponseDto>> ObterTodosAsync()
    {
        var ataques = await repository.ObterTodosAsync();
        return ataques.Select(AtaqueMapper.MapToResponseDto).ToList();
    }

    public async Task<List<AtaqueResponseDto>> ObterPorTipoAsync(Tipo tipo)
    {
        var ataques = await repository.ObterPorTipoAsync(tipo);
        return ataques.Select(AtaqueMapper.MapToResponseDto).ToList();
    }

    public async Task<AtaqueResponseDto> AtualizarAsync(Guid ataqueId, AtaqueCreationDto ataque)
    {
        if (!Enum.TryParse<Tipo>(ataque.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException(ExceptionMessages.TipoInvalido(ataque.Tipo));
        
        var ataqueAntigo = await repository.ObterPorIdAsync(ataqueId)
            ?? throw new NotFoundException(ExceptionMessages.AtaqueNaoEncontrado(ataqueId));
        
        ataqueAntigo.Nome = ataque.Nome;
        ataqueAntigo.Tipo = tipoConvertido;
        ataqueAntigo.Poder = ataque.Poder;
        ataqueAntigo.Precisao = ataque.Precisao;
        ataqueAntigo.QuantUsos = ataque.QuantUsos;
        
        var ataqueAtualizado = await repository.AtualizarESalvarAsync(ataqueAntigo);
        return AtaqueMapper.MapToResponseDto(ataqueAtualizado);
    }

    public async Task RemoverAsync(Guid id)
    {
        var ataque = await repository.ObterPorIdAsync(id) 
                     ?? throw new NotFoundException(ExceptionMessages.AtaqueNaoEncontrado(id));
        await repository.RemoverESalvarAsync(ataque);
    }
}