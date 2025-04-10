using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Crosscutting.Responses;
using BatalhaDePokemons.Crosscutting.SwaggerExamples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace BatalhaDePokemons.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController(IPokemonService service) : ControllerBase
{
    /// <summary>
    /// Cria um pokemon
    /// </summary>
    /// <param name="pokemon">Dados do pokemon</param>
    /// <response code="201">Pokemon criado com sucesso</response>
    /// <response code="400">Falha na criação do pokemon</response>
    [AllowAnonymous]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(PokemonCreationDto), typeof(PokemonCreationDtoExample))]
    [HttpPost]
    public async Task<IActionResult> CriarPokemon(PokemonCreationDto pokemon)
    {
        var pokemonCriadoId = await service.CriarAsync(pokemon);
        return CreatedAtAction(nameof(ObterPokemon), new { pokemonId = pokemonCriadoId }, pokemonCriadoId);
    }

    /// <summary>
    /// Procura um pokemon pelo id
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <response code="200">Pokemon encontrado</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<PokemonResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpGet("{pokemonId:guid}")]
    public async Task<IActionResult> ObterPokemon(Guid pokemonId)
    {
         var pokemon = await service.ObterPorIdAsync(pokemonId); 
         return Ok(pokemon);
    }
    
    /// <summary>
    /// Obtem todos os pokemons cadastrados
    /// </summary>
    /// <response code="200">Lista com todos os pokemons</response>
    /// <response code="400">Erro ao obter pokemons</response>
    [AllowAnonymous]
    [ProducesResponseType<List<PokemonResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> ObterTodosPokemons()
    {
        var pokemons = await service.ObterTodosComAtaquesAsync();
        return Ok(pokemons);
    }
    
    /// <summary>
    /// Obtem os ataques de um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <response code="200">Retorna os ataques do pokemon.</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<List<AtaqueResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpGet("{pokemonId:guid}/ataques")]
    public async Task<IActionResult> ObterAtaquesPokemon(Guid pokemonId)
    {
        var ataques = await service.ObterAtaquesPorPokemonIdAsync(pokemonId);
        return Ok(ataques);
    }
    
    
    /// <summary>
    /// Atualiza os dados de um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <param name="pokemon">Novos dados do pokemon</param>
    /// <response code="200">Pokemon atualizado</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<PokemonResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpPut("{pokemonId:guid}")]
    public async Task<IActionResult> AtualizarPokemon(Guid pokemonId, PokemonCreationDto pokemon)
    {
        var pokemonAtualizado = await service.AtualizarAsync(pokemonId, pokemon);
        return Ok(pokemonAtualizado);
    }
    
    /// <summary>
    /// Deleta um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <response code="204">Pokemon excluido com sucesso</response>
    /// <response code="404">Pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpDelete("{pokemonId:guid}")]
    public async Task<IActionResult> DeletarPokemon(Guid pokemonId)
    {
        await service.RemoverAsync(pokemonId);
        return NoContent();
    }

    /// <summary>
    /// Altera o hp atual do pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <param name="novoPontosDeVida">Nova quantidade de vida para o pokemon</param>
    /// <response code="200">Quantidade de vida do pokemon atualizado com sucesso</response>
    /// <response code="404">Pokemon não encontrado</response>
    /// /// <response code="400">Erro ao realizar cura</response>
    [AllowAnonymous]
    [ProducesResponseType<PokemonResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpPatch("{pokemonId:guid}/curar")]
    public async Task<IActionResult> CurarPokemon(Guid pokemonId, int novoPontosDeVida)
    {
        var pokemon = await service.CurarPokemonAsync(pokemonId, novoPontosDeVida);
        return Ok(pokemon);
    }
    
    /// <summary>
    /// Vincula um ataque a um pokemon
    /// </summary>
    /// <param name="pokemonId">Guid do pokemon</param>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <response code="200">Ataque vinculado com sucesso</response>
    /// <response code="404">Ataque ou pokemon não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpPatch("{pokemonId:guid}/ataques/{ataqueId:guid}")]
    public async Task<IActionResult> VincularAtaque(Guid pokemonId, Guid ataqueId)
    {
        await service.VincularAtaqueAsync(pokemonId, ataqueId);
        return Ok();
    }
}