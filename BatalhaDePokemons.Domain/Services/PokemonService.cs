using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
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

    public async Task<PokemonResponseDto?> ObterPorIdAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(id));
        return PokemonMapper.MapToResponseDto(pokemon);
    }

    public async Task<List<PokemonResponseDto>> ObterTodosAsync()
    {
        var pokemons = await repository.FindAllAsync();
        return pokemons.Select(PokemonMapper.MapToResponseDto).ToList();
    }

    public async Task<PokemonResponseDto> AtualizarAsync(Guid pokemonId, PokemonCreationDto pokemon)
    {
        if (!Enum.TryParse<Tipo>(pokemon.Tipo, true, out var tipoConvertido))
            throw new InvalidArgumentException(ExceptionMessages.TipoInvalido(pokemon.Tipo));

        var pokemonAntigo = await repository.FindByIdAsync(pokemonId)
                         ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(pokemonId));
        
        pokemonAntigo.Nome = pokemon.Nome;
        pokemonAntigo.Level = pokemon.Level;
        pokemonAntigo.Tipo = tipoConvertido;
        pokemonAntigo.Status.Hp = pokemon.Hp;
        pokemonAntigo.Status.Spd = pokemon.Spd;
        pokemonAntigo.Status.Def = pokemon.Def;
        pokemonAntigo.Status.Atk = pokemon.Atk;
        
        var pokemonAtualizado = await repository.UpdateAndCommitAsync(pokemonAntigo);
        return PokemonMapper.MapToResponseDto(pokemonAtualizado);
    }

    public async Task RemoverAsync(Guid id)
    {
        var pokemon = await repository.FindByIdAsync(id)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(id));
        await repository.RemoveAndCommitAsync(pokemon);
    }

    public async Task VincularAtaqueAsync(Guid pokemonId, Guid ataqueId)
    {
        var pokemon = await repository.FindByIdAsync(pokemonId)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(pokemonId));

        var ataque = await ataqueRepository.FindByIdAsync(ataqueId);
        
        if (ataque == null)
            throw new NotFoundException(ExceptionMessages.AtaqueNaoEncontrado(ataqueId));

        if (pokemon.PokemonAtaques.Count >= 4)
            throw new MaxAtaquesException(ExceptionMessages.MaxAtaquesPossivel);

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
                      ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(pokemonId));

        return pokemon.PokemonAtaques
            .Select(pa => AtaqueMapper.MapToResponseDto(pa.Ataque))
            .ToList();
    }

    public async Task<PokemonResponseDto> ObterPorIdComAtaquesAsync(Guid id)
    {
        var pokemon = await repository.FindByIdWithAtaquesAsync(id)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(id));
        
        return PokemonMapper.MapToResponseDto(pokemon);
    }

}