using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Dtos.Pokemon;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BatalhaDePokemons.Application.Services;
public class PokemonService(IPokemonRepository repository, IServiceProvider serviceProvider) : IPokemonService
{
    public async Task<PokemonResponseDto> CriarAsync(PokemonCreationDto pokemon)
    {
        var newPokemon = new Pokemon(
            pokemon.Name, pokemon.Level, pokemon.Tipo, pokemon.Hp, pokemon.Spd, pokemon.Def, pokemon.Atk);
        await repository.AddAsync(newPokemon);
        return newPokemon;
    }

    public async Task<PokemonResponseDto?> ObterPorIdAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id);
        return pokemon;
    }

    public async Task<ICollection<PokemonResponseDto>> ObterTodosAsync()
    {
        var pokemons = await repository.FindAllAsync();
        return pokemons.Select(pokemon => (PokemonResponseDto)pokemon).ToList();
    }

    public async Task<PokemonResponseDto?> AtualizarAsync(Guid pokemonId, PokemonCreationDto pokemon)
    {
        var oldPokemon = await repository.FindByIdAsync(pokemonId);
        if (oldPokemon == null) return null;
        oldPokemon.Name = pokemon.Name;
        oldPokemon.Level = pokemon.Level;
        oldPokemon.Tipo = pokemon.Tipo;
        oldPokemon.Status.Hp = pokemon.Hp;
        oldPokemon.Status.Spd = pokemon.Spd;
        oldPokemon.Status.Def = pokemon.Def;
        oldPokemon.Status.Atk = pokemon.Atk;
        
        await repository.UpdateAsync(oldPokemon);
        return oldPokemon;
    }

    public async Task RemoverAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id);
        if (pokemon == null) return;
        await repository.RemoveAsync(pokemon);
    }

    public async Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId)
    {
        var ataqueRepository = serviceProvider.GetRequiredService<IAtaqueRepository>();
        var pokemonAtaqueRepository = serviceProvider.GetRequiredService<IPokemonAtaqueRepository>();
        
        var pokemon = await repository.FindByIdAsync(pokemonId);
        if (pokemon == null) return;
        
        var ataque = await ataqueRepository.FindByIdAsync(ataqueId);
        if (ataque == null) return;

        if (pokemon.PokemonAtaques.Count >= 4)
            return;

        var pokemonAtaque = new PokemonAtaque
        {
            PokemonId = pokemonId,
            AtaqueId = ataqueId
        };

        await pokemonAtaqueRepository.AddAsync(pokemonAtaque);
    }
    
    public async Task<ICollection<AtaqueResponseDto>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId)
    {
        var pokemon = await repository.FindByIdWithAtaquesAsync(pokemonId);
        if (pokemon == null) return [];

        return pokemon.PokemonAtaques
            .Select(pa => (AtaqueResponseDto) pa.Ataque)
            .ToList();
    }

}