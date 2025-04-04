using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Domain.Services;
public class PokemonService(IPokemonRepository repository, IAtaqueRepository ataqueRepository, IPokemonAtaqueRepository pokemonAtaqueRepository) : IPokemonService
{
    public async Task<Guid> CriarAsync(PokemonCreationDto pokemon)
    {
        if (!Enum.TryParse<Tipo>(pokemon.Tipo, true, out var tipoConvertido))
            throw new ArgumentException($"Tipo {pokemon.Tipo} é inválido.");
        
        var novoPokemon = new Pokemon(
            pokemon.Nome, pokemon.Level, tipoConvertido, pokemon.Hp, pokemon.Spd, pokemon.Def, pokemon.Atk);
        var novoPokemonId = await repository.AddAndCommitAsync(novoPokemon);
        return novoPokemonId;
    }

    public async Task<PokemonResponseDto?> ObterPorIdAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id)
            ?? throw new NotFoundException($"O pokemon com id {id} não foi encontrado.");
        return pokemon.ToResponseDto();
    }

    public async Task<List<PokemonResponseDto>> ObterTodosAsync()
    {
        var pokemons = await repository.FindAllAsync();
        return pokemons.Select(pokemon => pokemon.ToResponseDto()).ToList();
    }

    public async Task<PokemonResponseDto> AtualizarAsync(Guid pokemonId, PokemonCreationDto pokemon)
    {
        if (!Enum.TryParse<Tipo>(pokemon.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException($"Tipo {pokemon.Tipo} é inválido");

        var pokemonAntigo = await repository.FindByIdAsync(pokemonId)
                         ?? throw new NotFoundException($"Pokemon com id {pokemonId} não foi encontrado.");
        
        pokemonAntigo.Nome = pokemon.Nome;
        pokemonAntigo.Level = pokemon.Level;
        pokemonAntigo.Tipo = tipoConvertido;
        pokemonAntigo.Status.Hp = pokemon.Hp;
        pokemonAntigo.Status.Spd = pokemon.Spd;
        pokemonAntigo.Status.Def = pokemon.Def;
        pokemonAntigo.Status.Atk = pokemon.Atk;
        
        var pokemonAtualizado = await repository.UpdateAndCommitAsync(pokemonAntigo);
        return pokemonAtualizado.ToResponseDto();
    }

    public async Task RemoverAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id)
            ?? throw new NotFoundException($"Pokemon com o id {id} não foi encontrado.");
        await repository.RemoveAndCommitAsync(pokemon);
    }

    public async Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId)
    {
        var pokemon = await repository.FindByIdAsync(pokemonId)
            ?? throw new NotFoundException($"O pokemon com id {pokemonId} não foi encontrado.");

        var ataque = await ataqueRepository.FindByIdAsync(ataqueId);
        
        if (ataque == null)
            throw new NotFoundException($"O ataque com id {ataqueId} não foi encontrado.");

        if (pokemon.PokemonAtaques.Count >= 4)
            throw new MaxAtaquesException("Este pokemon já possui o máximo de ataques possível.");

        var pokemonAtaque = new PokemonAtaque
        {
            PokemonId = pokemonId,
            AtaqueId = ataqueId
        };

        await pokemonAtaqueRepository.AddAndCommitAsync(pokemonAtaque);
    }
    
    public async Task<List<AtaqueResponseDto>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId)
    {
        var pokemon = await repository.FindByIdWithAtaquesAsync(pokemonId)
                      ?? throw new NotFoundException($"Pokemon com o id {pokemonId} não foi encontrado.");

        return pokemon.PokemonAtaques
            .Select(pa => pa.Ataque.MapToResponseDto())
            .ToList();
    }

}