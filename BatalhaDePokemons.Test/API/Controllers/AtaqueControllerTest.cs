using BatalhaDePokemons.API.Controllers;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Test.Domain.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BatalhaDePokemons.Test.API.Controllers;

public class AtaqueControllerTest
{
    private readonly AtaqueController _ataqueController;
    private readonly Mock<IAtaqueService> _ataqueServiceMock = new();

    public AtaqueControllerTest()
    {
        _ataqueController = new AtaqueController(_ataqueServiceMock.Object);
    }

    #region CriarAtaque

    [Fact]
    public async Task CriarAtaque_QuandoValido_DeveRetornar201Created()
    {
        var ataque = AtaqueBuilder.Novo().Build();

        _ataqueServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<AtaqueCreationDto>()))
            .ReturnsAsync(ataque);

        var result = await _ataqueController.CriarAtaque(ataque);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var returnedAtaque = Assert.IsType<AtaqueResponseDto>(createdResult.Value);
        Assert.Equal(ataque.Name, returnedAtaque.Name);
    }

    [Fact]
    public async Task CriarAtaque_QuandoErro_DeveRetornar400BadRequest()
    {
        _ataqueServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<AtaqueCreationDto>()))
            .ThrowsAsync(new Exception("Erro ao criar ataque"));

        var result = await _ataqueController.CriarAtaque(new AtaqueCreationDto());
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro ao criar ataque", badRequestResult.Value);
    }

    #endregion

    #region ObterAtaque

    [Fact]
    public async Task ObterAtaque_QuandoValido_DeveRetornar200Ok()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ataque);

        var result = await _ataqueController.ObterAtaque(ataque.AtaqueId);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaque = Assert.IsType<AtaqueResponseDto>(okResult.Value);
        Assert.Equal(ataque.Name, returnedAtaque.Name);
    }

    [Fact]
    public async Task ObterAtaque_QuandoNaoEncontrado_DeveRetornar404NotFound()
    {
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as AtaqueResponseDto);

        var result = await _ataqueController.ObterAtaque(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region ObterTodosAtaques

    [Fact]
    public async Task ObterTodosAtaques_QuandoValido_DeveRetornar200OK()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueBuilder.Novo().Build(),
            AtaqueBuilder.Novo().Build()
        };
        _ataqueServiceMock
            .Setup(s => s.ObterTodosAsync())
            .ReturnsAsync(ataques);

        var result = await _ataqueController.ObterTodosAtaques();
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaques = Assert.IsType<List<AtaqueResponseDto>>(okResult.Value);
        Assert.Equal(ataques.Count, returnedAtaques.Count);
    }

    [Fact]
    public async Task ObterTodosAtaques_QuandoErro_DeveRetornar400BadRequest()
    {
        _ataqueServiceMock
            .Setup(s=>s.ObterTodosAsync())
            .ThrowsAsync(new Exception("Erro ao obter ataques"));

        var result = await _ataqueController.ObterTodosAtaques();
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro ao obter ataques", badRequestResult.Value);
    }

    #endregion

    #region ObterAtaquesPorTipo

    [Fact]
    public async Task ObterAtaquesPorTipo_QuandoValido_DeveRetornar200OK()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueBuilder.Novo().Build(),
            AtaqueBuilder.Novo().Build()
        };
        _ataqueServiceMock
            .Setup(s=>s.ObterPorTipoAsync(It.IsAny<Tipo>()))
            .ReturnsAsync(ataques);
        
        var result = await _ataqueController.ObterAtaquesPorTipo(It.IsAny<Tipo>());
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaques = Assert.IsType<List<AtaqueResponseDto>>(okResult.Value);
        Assert.Equal(ataques.Count, returnedAtaques.Count);
    }

    [Fact]
    public async Task ObterAtaquesPorTipo_QuandoErro_DeveRetornar400BadRequest()
    {
        _ataqueServiceMock
            .Setup(s=>s.ObterPorTipoAsync(It.IsAny<Tipo>()))
            .ThrowsAsync(new Exception("Erro ao obter ataques"));
        
        var result = await _ataqueController.ObterAtaquesPorTipo(It.IsAny<Tipo>());
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro ao obter ataques", badRequestResult.Value);
    }

    #endregion

    #region AtualizarAtaque

    [Fact]
    public async Task AtualizarAtaque_QuandoValido_DeveRetornar200Ok()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<AtaqueCreationDto>()))
            .ReturnsAsync(ataque);

        var result = await _ataqueController.AtualizarAtaque(ataque.AtaqueId, ataque);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaque = Assert.IsType<AtaqueResponseDto>(okResult.Value);
        Assert.Equal(ataque.Name, returnedAtaque.Name);
    }

    [Fact]
    public async Task AtualizarAtaque_QuandoNaoEncontrado_DeveRetornar404NotFound()
    {
        _ataqueServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<AtaqueCreationDto>()))
            .ReturnsAsync(null as AtaqueResponseDto);

        var result = await _ataqueController.AtualizarAtaque(Guid.NewGuid(), new AtaqueCreationDto());
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region DeletarAtaque

    [Fact]
    public async Task DeletarAtaque_QuandoValido_DeveRetornar204NoContent()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ataque);

        var result = await _ataqueController.DeletarAtaque(ataque.AtaqueId);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeletarAtaque_QuandoNaoEncontrado_DeveRetornar404NotFound()
    {
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as AtaqueResponseDto);

        var result = await _ataqueController.DeletarAtaque(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion
}
