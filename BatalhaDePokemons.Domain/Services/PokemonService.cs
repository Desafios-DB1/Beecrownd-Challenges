using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Mappers;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Domain.Services;
public class PokemonService(IPokemonRepository repository, IAtaqueRepository ataqueRepository, IPokemonAtaqueRepository pokemonAtaqueRepository) : IPokemonService
{
    public async Task<Guid> CriarAsync(PokemonCreationDto pokemon)
    {
        if (!Enum.TryParse<Tipo>(pokemon.Tipo, true, out var tipoConvertido))
            throw new ArgumentException(ExceptionMessages.TipoInvalido(pokemon.Tipo));
        
        var novoPokemon = new Pokemon(
            pokemon.Nome, pokemon.Level, tipoConvertido, pokemon.Hp, pokemon.Spd, pokemon.Def, pokemon.Atk);
        var novoPokemonId = await repository.AddAndCommitAsync(novoPokemon);
        return novoPokemonId;
    }
    
    public async Task<PokemonResponseDto> ObterPorIdAsync(Guid id)
    {
        var pokemon = await ValidarPokemon(id);
        return PokemonMapper.MapToResponseDto(pokemon);
    }

    public async Task<List<PokemonResponseDto>> ObterTodosAsync()
    {
        var pokemons = await repository.FindAllAsync();
        return pokemons.Select(PokemonMapper.MapToResponseDto).ToList();
    }

    public async Task<List<PokemonResponseDto>> ObterTodosComAtaquesAsync()
    {
        var pokemons = await repository.FindAllWithAtaquesAsync();
        return pokemons.Select(PokemonMapper.MapToResponseDto).ToList();
    }

    public async Task<PokemonResponseDto> AtualizarAsync(Guid pokemonId, PokemonCreationDto pokemon)
    {
        if (!Enum.TryParse<Tipo>(pokemon.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException(ExceptionMessages.TipoInvalido(pokemon.Tipo));

        var pokemonAntigo = await ValidarPokemon(pokemonId);
        
        pokemonAntigo.Nome = pokemon.Nome;
        pokemonAntigo.Level = pokemon.Level;
        pokemonAntigo.Tipo = tipoConvertido;
        pokemonAntigo.IsDesmaiado = pokemon.IsDesmaiado;
        pokemonAntigo.Status.Hp = pokemon.Hp;
        pokemonAntigo.Status.Spd = pokemon.Spd;
        pokemonAntigo.Status.Def = pokemon.Def;
        pokemonAntigo.Status.Atk = pokemon.Atk;
        
        var pokemonAtualizado = await repository.UpdateAndCommitAsync(pokemonAntigo);
        return PokemonMapper.MapToResponseDto(pokemonAtualizado);
    }

    public async Task RemoverAsync(Guid id)
    {
        var pokemon = await ValidarPokemon(id);
        await repository.RemoveAndCommitAsync(pokemon);
    }

    public async Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId)
    {
        var pokemon = await ValidarPokemon(pokemonId);

        var ataque = await ataqueRepository.FindByIdAsync(ataqueId)
            ?? throw new NotFoundException(ExceptionMessages.AtaqueNaoEncontrado(ataqueId));

        pokemon.VerificarSePodeAprenderAtaque(ataque);

        var pokemonAtaque = new PokemonAtaque
        {
            PokemonId = pokemonId,
            AtaqueId = ataqueId
        };

        await pokemonAtaqueRepository.AddAndCommitAsync(pokemonAtaque);
    }
    
    public async Task<List<AtaqueResponseDto>> ObterAtaquesPorPokemonIdAsync(Guid pokemonId)
    {
        var pokemon = await ValidarPokemon(pokemonId);

        return pokemon.PokemonAtaques
            .Select(pa => AtaqueMapper.MapToResponseDto(pa.Ataque))
            .ToList();
    }
    public async Task<PokemonResponseDto> CurarPokemonAsync(Guid pokemonId, int newHp)
    {
        var pokemon = await ValidarPokemon(pokemonId);
        pokemon.Curar(newHp);
        await repository.UpdateAndCommitAsync(pokemon);
        return PokemonMapper.MapToResponseDto(pokemon);
    }
    private async Task<Pokemon> ValidarPokemon(Guid id)
    {
        var pokemon = await repository.FindByIdWithAtaquesAsync(id)
                      ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(id));
        return pokemon;
    }
}