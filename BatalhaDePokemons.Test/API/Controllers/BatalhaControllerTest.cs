using BatalhaDePokemons.API.Controllers;
using BatalhaDePokemons.Crosscutting.Dtos.Batalha;
using BatalhaDePokemons.Crosscutting.Dtos.Turno;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BatalhaDePokemons.Test.API.Controllers;

public class BatalhaControllerTest
{
    private readonly BatalhaController _batalhaController;
    private readonly Mock<IBatalhaService> _batalhaServiceMock = new();

    public BatalhaControllerTest()
    {
        _batalhaController = new BatalhaController(_batalhaServiceMock.Object);
    }

    #region IniciarBatalha

    [Fact]
    public async Task IniciarBatalha_QuandoValido_DeveRetornar200Ok()
    {
        var dto = new IniciarBatalhaDto { AtacanteId = Guid.NewGuid(), DefensorId = Guid.NewGuid() };
        var batalhaId = Guid.NewGuid();

        _batalhaServiceMock
            .Setup(s => s.IniciarBatalha(dto.AtacanteId, dto.DefensorId))
            .ReturnsAsync(batalhaId);

        var result = await _batalhaController.IniciarBatalha(dto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(batalhaId, okResult.Value);
    }

    [Fact]
    public async Task IniciarBatalha_QuandoErro_DeveLancarException()
    {
        var dto = new IniciarBatalhaDto { AtacanteId = Guid.NewGuid(), DefensorId = Guid.NewGuid() };

        _batalhaServiceMock
            .Setup(s => s.IniciarBatalha(dto.AtacanteId, dto.DefensorId))
            .ThrowsAsync(new InvalidArgumentException("Pokémon inválido"));

        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(() =>
            _batalhaController.IniciarBatalha(dto));

        Assert.Equal("Pokémon inválido", exception.Message);
    }

    #endregion

    #region ExecutarTurno

    [Fact]
    public async Task ExecutarTurno_QuandoValido_DeveRetornar200Ok()
    {
        var batalhaId = Guid.NewGuid();
        var dto = new ExecutarTurnoDto { AtacanteId = Guid.NewGuid(), AtaqueId = Guid.NewGuid() };

        var result = await _batalhaController.ExecutarTurnos(batalhaId, dto);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task ExecutarTurno_QuandoErro_DeveLancarException()
    {
        var batalhaId = Guid.NewGuid();
        var dto = new ExecutarTurnoDto { AtacanteId = Guid.NewGuid(), AtaqueId = Guid.NewGuid() };

        _batalhaServiceMock
            .Setup(s => s.ExecutarTurno(batalhaId, dto.AtacanteId, dto.AtaqueId))
            .ThrowsAsync(new NotFoundException("Ataque não encontrado"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _batalhaController.ExecutarTurnos(batalhaId, dto));

        Assert.Equal("Ataque não encontrado", exception.Message);
    }

    #endregion

    #region EncerrarBatalha

    [Fact]
    public async Task EncerrarBatalha_QuandoValido_DeveRetornar200Ok()
    {
        var batalhaId = Guid.NewGuid();
        var dto = new EncerrarBatalhaDto { PokemonDesistenteId = Guid.NewGuid() };

        var result = await _batalhaController.EncerrarBatalha(batalhaId, dto);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task EncerrarBatalha_QuandoErro_DeveLancarNotFoundException()
    {
        var batalhaId = Guid.NewGuid();
        var dto = new EncerrarBatalhaDto { PokemonDesistenteId = Guid.NewGuid() };

        _batalhaServiceMock
            .Setup(s => s.EncerrarBatalhaAsync(batalhaId, dto.PokemonDesistenteId))
            .ThrowsAsync(new NotFoundException("Batalha não encontrada"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _batalhaController.EncerrarBatalha(batalhaId, dto));

        Assert.Equal("Batalha não encontrada", exception.Message);
    }

    #endregion

    #region ObterBatalha

    [Fact]
    public async Task ObterBatalha_QuandoValido_DeveRetornar200Ok()
    {
        var batalhaId = Guid.NewGuid();
        var dto = new BatalhaDetalhadaDto() { BatalhaId = batalhaId };

        _batalhaServiceMock
            .Setup(s => s.ObterBatalhaDetalhadaAsync(batalhaId))
            .ReturnsAsync(dto);

        var result = await _batalhaController.ObterBatalha(batalhaId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(dto, okResult.Value);
    }

    [Fact]
    public async Task ObterBatalha_QuandoNaoEncontrada_DeveLancarNotFoundException()
    {
        var batalhaId = Guid.NewGuid();

        _batalhaServiceMock
            .Setup(s => s.ObterBatalhaDetalhadaAsync(batalhaId))
            .ThrowsAsync(new NotFoundException("Batalha não encontrada"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _batalhaController.ObterBatalha(batalhaId));

        Assert.Equal("Batalha não encontrada", exception.Message);
    }

    #endregion

    #region ObterHistoricoDeBatalhas

    [Fact]
    public async Task ObterHistoricoDeBatalhas_QuandoValido_DeveRetornar200Ok()
    {
        var batalhas = new List<BatalhaResponseDto>
        {
            new() { BatalhaId = Guid.NewGuid() },
            new() { BatalhaId = Guid.NewGuid() }
        };

        _batalhaServiceMock
            .Setup(s => s.ObterTodasBatalhas())
            .ReturnsAsync(batalhas);

        var result = await _batalhaController.ObterHistoricoDeBatalhas();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(batalhas, okResult.Value);
    }

    [Fact]
    public async Task ObterHistoricoDeBatalhas_QuandoErro_DeveLancarException()
    {
        _batalhaServiceMock
            .Setup(s => s.ObterTodasBatalhas())
            .ThrowsAsync(new Exception("Erro ao obter batalhas"));

        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _batalhaController.ObterHistoricoDeBatalhas());

        Assert.Equal("Erro ao obter batalhas", exception.Message);
    }

    #endregion
}
