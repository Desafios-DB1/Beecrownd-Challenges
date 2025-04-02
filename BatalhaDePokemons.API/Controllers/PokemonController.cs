using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Dtos.Pokemon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BatalhaDePokemons.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController(IPokemonService service) : ControllerBase
{
    /// <summary>
    /// Cria um pokemon
    /// </summary>
    /// <param name="pokemon">Dados do pokemon</param>
    /// <response code="201">Pokemon criado</response>
    /// <response code="400">Falha na criação</response>
    [AllowAnonymous]
    [ProducesResponseType<PokemonResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CriarPokemon(PokemonCreationDto pokemon)
    {
        try
        {
            var created = await service.CriarAsync(pokemon);
            return CreatedAtAction(nameof(ObterPokemon), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Vincula um ataque a um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <response code="204">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{pokemonId:guid}/ataques/{ataqueId:guid}")]
    public async Task<IActionResult> VincularAtaque(Guid pokemonId, Guid ataqueId)
    {
        try
        {
            await service.VincularAtaqueAsync(pokemonId, ataqueId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Procura um pokemon pelo id
    /// </summary>
    /// <param name="id">Guid do pokemon</param>
    /// <response code="200">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<PokemonResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPokemon(Guid id)
    {
        try
        {
            var pokemon = await service.ObterPorIdAsync(id);
            if ( pokemon == null ) return NotFound();
            return Ok(pokemon);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Obtem os ataques de um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <response code="204">Retorna os ataques</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<ICollection<AtaqueResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{pokemonId:guid}/ataques")]
    public async Task<IActionResult> ObterAtaquesPokemon(Guid pokemonId)
    {
        try
        {
            var ataques = await service.ObterAtaquesPorPokemonIdAsync(pokemonId);
            return Ok(ataques);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Obtem todos os pokemons cadastrados
    /// </summary>
    /// <response code="200">Sucesso</response>
    /// <response code="400">Erro ao obter pokemons</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> ObterTodosPokemons()
    {
        try
        {
            var pokemons = await service.ObterTodosAsync();
            return Ok(pokemons);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Atualiza os dados de um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <param name="pokemon">Novos dados do pokemon</param>
    /// <response code="200">Pokemon atualizado</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<AtaqueResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{pokemonId:guid}")]
    public async Task<IActionResult> AtualizarPokemon(Guid pokemonId, PokemonCreationDto pokemon)
    {
        try
        {
            var updatedPokemon = await service.AtualizarAsync(pokemonId, pokemon);
            if (updatedPokemon is null) return NotFound();
            return Ok(updatedPokemon);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Deleta um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <response code="204">Pokemon excluido</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{pokemonId:guid}")]
    public async Task<IActionResult> DeletarPokemon(Guid pokemonId)
    {
        try
        {
            var pokemon = await service.ObterPorIdAsync(pokemonId);
            if (pokemon is null) return NotFound();
            await service.RemoverAsync(pokemonId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}