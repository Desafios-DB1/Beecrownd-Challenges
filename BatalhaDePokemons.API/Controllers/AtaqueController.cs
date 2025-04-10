using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Crosscutting.Responses;
using BatalhaDePokemons.Crosscutting.SwaggerExamples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace BatalhaDePokemons.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AtaqueController(IAtaqueService service) : ControllerBase
{
    /// <summary>
    /// Cria um ataque
    /// </summary>
    /// <param name="ataque">Dados do ataque</param>
    /// <response code="201">Ataque criado com sucesso</response>
    /// <response code="400">Falha na criação do ataque</response>
    [AllowAnonymous]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(AtaqueCreationDto), typeof(AtaqueCreationDtoExample))]
    [HttpPost]
    public async Task<IActionResult> CriarAtaque(AtaqueCreationDto ataque)
    {
        var ataqueCriadoId = await service.CriarAsync(ataque);
        return CreatedAtAction(nameof(ObterAtaque), new { ataqueId = ataqueCriadoId}, ataqueCriadoId);
    }

    /// <summary>
    /// Procura um ataque pelo guid
    /// </summary>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <response code="200">Ataque encontrado</response>
    /// <response code="404">Ataque não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<AtaqueResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpGet("{ataqueId:guid}")]
    public async Task<IActionResult> ObterAtaque(Guid ataqueId)
    {
        var ataque = await service.ObterPorIdAsync(ataqueId);
        return Ok(ataque);
    }
    
    /// <summary>
    /// Obtem os ataques filtrados por tipo
    /// </summary>
    /// <param name="tipo">Tipo de ataque</param>
    /// <response code="200">Ataques do tipo especificado</response>
    /// <response code="400">Erro ao obter ataques</response>
    [AllowAnonymous]
    [ProducesResponseType<List<AtaqueResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpGet("{tipo}")]
    public async Task<IActionResult> ObterAtaquesPorTipo(Tipo tipo)
    {
        var ataques = await service.ObterPorTipoAsync(tipo);
        return Ok(ataques);
    }
    
    /// <summary>
    /// Obtem todos os ataques cadastrados
    /// </summary>
    /// <response code="200">Lista com todos os ataques</response>
    /// <response code="400">Erro ao obter ataques</response>
    [AllowAnonymous]
    [ProducesResponseType<List<AtaqueResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> ObterTodosAtaques()
    {
        var ataques = await service.ObterTodosAsync();
        return Ok(ataques);
    }

    /// <summary>
    /// Atualiza os dados de um ataque
    /// </summary>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <param name="ataque">Novos dados do ataque</param>
    /// <response code="200">Ataque atualizado</response>
    /// <response code="404">Ataque não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<AtaqueResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpPut("{ataqueId:guid}")]
    public async Task<IActionResult> AtualizarAtaque(Guid ataqueId, AtaqueCreationDto ataque)
    {
        var ataqueAtualizado = await service.AtualizarAsync(ataqueId, ataque);
        return Ok(ataqueAtualizado);
    }

    /// <summary>
    /// Deleta um ataque
    /// </summary>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <response code="204">Ataque excluido</response>
    /// <response code="404">Ataque não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{ataqueId:guid}")]
    public async Task<IActionResult> RemoverAtaque(Guid ataqueId)
    {
        await service.RemoverAsync(ataqueId);
        return NoContent();
    }
}