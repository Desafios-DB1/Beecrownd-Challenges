using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Application.Services;

public class AtaqueService(IAtaqueRepository repository) : IAtaqueService
{
    public async Task<AtaqueResponseDto> CriarAsync(AtaqueCreationDto ataque)
    {
        var newAtaque = new Ataque(
            ataque.Name, ataque.Tipo, ataque.Poder, ataque.Precisao, ataque.PP);
        await repository.AddAsync(newAtaque);
        return newAtaque;
    }

    public async Task<AtaqueResponseDto?> ObterPorIdAsync(Guid id)
    {
        var ataque = await repository.FindByIdAsync(id);
        if (ataque == null) return null;
        return ataque;
    }

    public async Task<ICollection<AtaqueResponseDto>> ObterTodosAsync()
    {
        var ataques = await repository.FindAllAsync();
        return ataques.Select(ataque => (AtaqueResponseDto)ataque).ToList();
    }

    public async Task<ICollection<AtaqueResponseDto>> ObterPorTipoAsync(Tipo tipo)
    {
        var ataques = await repository.FindByTipoAsync(tipo);
        return ataques.Select(ataque => (AtaqueResponseDto)ataque).ToList();
    }

    public async Task<AtaqueResponseDto?> AtualizarAsync(Guid ataqueId, AtaqueCreationDto ataque)
    {
        var updatedAtaque = await repository.FindByIdAsync(ataqueId);
        if (updatedAtaque == null) return null;
        updatedAtaque.Name = ataque.Name;
        updatedAtaque.Tipo = ataque.Tipo;
        updatedAtaque.Poder = ataque.Poder;
        updatedAtaque.Precisao = ataque.Precisao;
        updatedAtaque.PP = ataque.PP;
        
        await repository.UpdateAsync(updatedAtaque);
        return updatedAtaque;
    }

    public async Task RemoverAsync(Guid id)
    {
        var ataque = await repository.FindByIdAsync(id);
        if (ataque == null) return;
        await repository.RemoveAsync(ataque);
    }
}