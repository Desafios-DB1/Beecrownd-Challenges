using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType<AtaqueResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CriarAtaque(AtaqueCreationDto ataque)
    {
        try
        {
            var created = await service.CriarAsync(ataque);
            return CreatedAtAction(nameof(ObterAtaque), new { id = created.AtaqueId}, created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Procura um ataque pelo guid
    /// </summary>
    /// <param name="ataqueId">Guid do ataque</param>
    /// <response code="200">Ataque procurado</response>
    /// <response code="404">Ataque não encontrado</response>
    [AllowAnonymous]
    [ProducesResponseType<AtaqueResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{ataqueId:guid}")]
    public async Task<IActionResult> ObterAtaque(Guid ataqueId)
    {
        try
        {
            var ataque = await service.ObterPorIdAsync(ataqueId);
            if (ataque is null) return NotFound();
            return Ok(ataque);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Obtem todos os ataques cadastrados
    /// </summary>
    /// <response code="200">Lista com todos ataques</response>
    /// <response code="400">Erro ao obter ataques</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> ObterTodosAtaques()
    {
        try
        {
            var ataques = await service.ObterTodosAsync();
            return Ok(ataques);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Obtem os ataques filtrados por tipo
    /// </summary>
    /// <param name="tipo">Tipo de ataque</param>
    /// <response code="200">Ataques do tipo especificado</response>
    /// <response code="400">Erro ao obter ataques</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{tipo}")]
    public async Task<IActionResult> ObterAtaquesPorTipo(Tipo tipo)
    {
        try
        {
            var ataques = await service.ObterPorTipoAsync(tipo);
            return Ok(ataques);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{ataqueId:guid}")]
    public async Task<IActionResult> AtualizarAtaque(Guid ataqueId, AtaqueCreationDto ataque)
    {
        try
        {
            var oldAtaque = await service.AtualizarAsync(ataqueId, ataque);
            if (oldAtaque is null) return NotFound();
            return Ok(oldAtaque);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{ataqueId:guid}")]
    public async Task<IActionResult> DeletarAtaque(Guid ataqueId)
    {
        try
        {
            var ataque = await service.ObterPorIdAsync(ataqueId);
            if (ataque is null) return NotFound();
            await service.RemoverAsync(ataqueId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}