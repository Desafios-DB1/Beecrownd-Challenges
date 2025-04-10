using BatalhaDePokemons.Crosscutting.Dtos.Batalha;
using BatalhaDePokemons.Crosscutting.Dtos.Turno;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Crosscutting.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatalhaDePokemons.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BatalhaController(IBatalhaService batalhaService) : ControllerBase
{
    /// <summary>
    /// Inicia uma batalha entre dois pokemons
    /// </summary>
    /// <param name="dto">Dto contendo o Id dos dois pokemons que irão batalhar</param>
    /// <response code="200">Batalha iniciada com sucesso</response>
    /// <response code="400">Falha ao iniciar a batalha</response> 
    /// <response code="404">Um pokemon não existe</response>
    [AllowAnonymous]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> IniciarBatalha([FromBody] IniciarBatalhaDto dto)
    {
        var batalhaId = await batalhaService.IniciarBatalha(dto.AtacanteId, dto.DefensorId);
        return Ok(batalhaId);
    }
    
    /// <summary>
    /// Executa um turno da batalha dos pokemons
    /// </summary>
    /// <param name="dto">Dto contendo o ataque e o id do atacante</param>
    /// <param name="batalhaId">Id da batalha</param>
    /// <response code="200">Turno executado com sucesso</response>
    /// <response code="400">Falha ao executar o turno</response> 
    /// <response code="404">Um ataque ou pokemon não existe</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpPost("{batalhaId:guid}/turnos")]
    public async Task<IActionResult> ExecutarTurnos(Guid batalhaId, [FromBody] ExecutarTurnoDto dto)
    {
        await batalhaService.ExecutarTurno(batalhaId, dto.AtacanteId, dto.AtaqueId);
        return Ok();
    }

    /// <summary>
    /// Finaliza uma batalha entre pokemons
    /// </summary>
    /// <param name="dto">Dto contendo o id do pokemon que está desistindo da batalha</param>
    /// <param name="batalhaId">Id da batalha</param>
    /// <response code="200">Batalha encerrada com sucesso</response>
    /// <response code="400">Falha ao finalizar batalha</response> 
    /// <response code="404">Batalha não encontrada</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpPost("{batalhaId:guid}/encerrar")]
    public async Task<IActionResult> EncerrarBatalha(Guid batalhaId, EncerrarBatalhaDto dto)
    {
        await batalhaService.EncerrarBatalhaAsync(batalhaId, dto.PokemonDesistenteId);
        return Ok();
    }
    
    /// <summary>
    /// Obtem as informações atuais de uma batalha
    /// </summary>
    /// <param name="batalhaId">Id da batalha</param>
    /// <response code="200">Dados da batalha</response>
    /// <response code="400">Falha ao encontrar batalha</response> 
    /// <response code="404">Batalha não encontrada</response>
    [AllowAnonymous]
    [ProducesResponseType<BatalhaResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [HttpGet("{batalhaId:guid}")]
    public async Task<IActionResult> ObterBatalha(Guid batalhaId)
    {
        var dadosBatalha = await batalhaService.ObterBatalhaDetalhadaAsync(batalhaId);
        return Ok(dadosBatalha);
    }

    /// <summary>
    /// Obtem todas as batalhas registradas
    /// </summary>
    /// <response code="200">Lista com os dados das batalhas</response>
    /// <response code="400">Falha ao encontrar batalhas</response> 
    [AllowAnonymous]
    [ProducesResponseType<List<BatalhaResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [HttpGet("/historico")]
    public async Task<IActionResult> ObterHistoricoDeBatalhas()
    {
        var batalhas = await batalhaService.ObterTodasBatalhas();
        return Ok(batalhas);
    }
}