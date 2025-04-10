using BatalhaDePokemons.API.Controllers;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Mappers;
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
            .ReturnsAsync(ataque.AtaqueId);

        var result = await _ataqueController.CriarAtaque(AtaqueMapper.MapToCreationDto(ataque));
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        
        var returnedAtaqueId = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(ataque.AtaqueId, returnedAtaqueId);
    }

    [Fact]
    public async Task CriarAtaque_QuandoErro_DeveLancarInvalidArgumentException()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<AtaqueCreationDto>()))
            .ThrowsAsync(new InvalidArgumentException("Tipo de ataque invalido"));

        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(() =>
            _ataqueController.CriarAtaque(AtaqueMapper.MapToCreationDto(ataque)));
        
        Assert.Equal("Tipo de ataque invalido", exception.Message);
    }

    #endregion

    #region ObterAtaque

    [Fact]
    public async Task ObterAtaque_QuandoValido_DeveRetornar200Ok()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(AtaqueMapper.MapToResponseDto(ataque));

        var result = await _ataqueController.ObterAtaque(ataque.AtaqueId);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaque = Assert.IsType<AtaqueResponseDto>(okResult.Value);
        Assert.Equal(ataque.Nome, returnedAtaque.Nome);
    }

    [Fact]
    public async Task ObterAtaque_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Ataque não encontrado"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(()=>
            _ataqueController.ObterAtaque(Guid.NewGuid()));

        Assert.Equal("Ataque não encontrado", exception.Message);
    }

    #endregion

    #region ObterTodosAtaques

    [Fact]
    public async Task ObterTodosAtaques_QuandoValido_DeveRetornar200OK()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build()),
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build())
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
    public async Task ObterTodosAtaques_QuandoErro_DeveLancarException()
    {
        _ataqueServiceMock
            .Setup(s=>s.ObterTodosAsync())
            .ThrowsAsync(new Exception("Erro ao obter ataques"));

        var exception = await Assert.ThrowsAsync<Exception>(()=>
            _ataqueController.ObterTodosAtaques());
        
        Assert.Equal("Erro ao obter ataques", exception.Message);
    }

    #endregion

    #region ObterAtaquesPorTipo

    [Fact]
    public async Task ObterAtaquesPorTipo_QuandoValido_DeveRetornar200OK()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build()),
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build())
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
    public async Task ObterAtaquesPorTipo_QuandoArgumentoInvalido_DeveLancarInvalidArgumentException()
    {
        _ataqueServiceMock
            .Setup(s=>s.ObterPorTipoAsync(It.IsAny<Tipo>()))
            .ThrowsAsync(new InvalidArgumentException("Tipo inválido"));

        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(()=>
            _ataqueController.ObterAtaquesPorTipo(It.IsAny<Tipo>()));
        
        Assert.Equal("Tipo inválido", exception.Message);
    }

    #endregion

    #region AtualizarAtaque

    [Fact]
    public async Task AtualizarAtaque_QuandoValido_DeveRetornar200Ok()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<AtaqueCreationDto>()))
            .ReturnsAsync(AtaqueMapper.MapToResponseDto(ataque));

        var result = await _ataqueController.AtualizarAtaque(ataque.AtaqueId, AtaqueMapper.MapToCreationDto(ataque));
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<AtaqueResponseDto>(okResult.Value);
    }

    [Fact]
    public async Task AtualizarAtaque_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<AtaqueCreationDto>()))
            .ThrowsAsync(new NotFoundException("Ataque não encontrado"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _ataqueController.AtualizarAtaque(Guid.NewGuid(), AtaqueMapper.MapToCreationDto(ataque)));
        
        Assert.Equal("Ataque não encontrado", exception.Message);
    }

    #endregion

    #region RemoverAtaque

    [Fact]
    public async Task DeletarAtaque_QuandoValido_DeveRetornar204NoContent()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(AtaqueMapper.MapToResponseDto(ataque));

        var result = await _ataqueController.RemoverAtaque(ataque.AtaqueId);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeletarAtaque_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        _ataqueServiceMock
            .Setup(s => s.RemoverAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Ataque não encontrado"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(()=>
            _ataqueController.RemoverAtaque(Guid.NewGuid()));
        
        Assert.Equal("Ataque não encontrado", exception.Message);
    }

    #endregion
}
