using BatalhaDePokemons.API.Controllers;
using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Dtos.Pokemon;
using BatalhaDePokemons.Application.Interfaces;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Test.Domain.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BatalhaDePokemons.Test.API.Controllers;

public class PokemonControllerTest
{
    private readonly PokemonController _pokemonController;
    private readonly Mock<IPokemonService> _pokemonServiceMock = new();

    public PokemonControllerTest()
    {
        _pokemonController = new PokemonController(_pokemonServiceMock.Object);
    }

    #region CriarPokemon

    [Fact]
    public async Task CriarPokemon_QuandoValido_DeveRetornar201Created()
    {
        // Arrange
        var pokemon = PokemonBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<PokemonCreationDto>()))
            .ReturnsAsync(pokemon);
        
        // Act
        var result = await _pokemonController.CriarPokemon(pokemon);
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var returnedPokemon = Assert.IsType<PokemonResponseDto>(createdResult.Value);
        Assert.Equal(pokemon.Name, returnedPokemon.Name);
    }

    [Fact]
    public async Task CriarPokemon_QuandoErro_DeveRetornar400BadRequest()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<PokemonCreationDto>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var result = await _pokemonController.CriarPokemon(pokemon);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro no banco de dados", badRequestResult.Value);
    }

    #endregion

    #region VincularAtaque

    [Fact]
    public async Task VincularAtaque_QuandoValido_DeveRetornar204NoContent()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        var ataque = AtaqueBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s=>s.VincularAtaqueAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);
        
        var result = await _pokemonController.VincularAtaque(pokemon.PokemonId, ataque.AtaqueId);
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async Task VincularAtaque_QuandoErro_DeveRetornar400BadRequest()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        var ataque = AtaqueBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s=>s.VincularAtaqueAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Pokemon não existe"));
        
        var result = await _pokemonController.VincularAtaque(pokemon.PokemonId, ataque.AtaqueId);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Pokemon não existe", badRequestResult.Value);
    }

    #endregion

    #region ObterPokemon

    [Fact]
    public async Task ObterPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(pokemon);
        
        var result = await _pokemonController.ObterPokemon(pokemon.PokemonId);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedPokemon = Assert.IsType<PokemonResponseDto>(okResult.Value);
        Assert.Equal(pokemon.Name, returnedPokemon.Name);
    }

    [Fact]
    public async Task ObterPokemon_QuandoNaoExiste_DeveRetornar404NotFound()
    {
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as PokemonResponseDto);
        
        var result = await _pokemonController.ObterPokemon(Guid.NewGuid());
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ObterPokemon_QuandoErro_DeveRetornar400BadRequest()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var result = await _pokemonController.ObterPokemon(pokemon.PokemonId);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro no banco de dados", badRequestResult.Value);
    }

    #endregion

    #region ObterAtaquesPokemon

    [Fact]
    public async Task ObterAtaquesPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueBuilder.Novo().Build(),
            AtaqueBuilder.Novo().Build()
        };
        _pokemonServiceMock
            .Setup(s => s.ObterAtaquesPorPokemonIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ataques);
        
        var result = await _pokemonController.ObterAtaquesPokemon(Guid.NewGuid());
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaques = Assert.IsAssignableFrom<List<AtaqueResponseDto>>(okResult.Value);
        Assert.Equal(ataques[0].Name, returnedAtaques[0].Name);
    }

    [Fact]
    public async Task ObterAtaquesPokemon_QuandoErro_DeveRetornar400BadRequest()
    {
        _pokemonServiceMock
            .Setup(s=>s.ObterAtaquesPorPokemonIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var result = await _pokemonController.ObterAtaquesPokemon(Guid.NewGuid());
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro no banco de dados", badRequestResult.Value);
    }

    #endregion

    #region AtualizarPokemon

    [Fact]
    public async Task AtualizarPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ReturnsAsync(pokemon);
        
        var result = await _pokemonController.AtualizarPokemon(pokemon.PokemonId, pokemon);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedPokemon = Assert.IsType<PokemonResponseDto>(okResult.Value);
        Assert.Equal(pokemon.Name, returnedPokemon.Name);
    }

    [Fact]
    public async Task AtualizarPokemon_QuandoNaoEncontrado_DeveRetornar404NotFound()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ReturnsAsync(null as PokemonResponseDto);
        
        var result = await _pokemonController.AtualizarPokemon(Guid.NewGuid(), pokemon);
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarPokemon_QuandoErro_DeveRetornar400BadRequest()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var result = await _pokemonController.AtualizarPokemon(Guid.NewGuid(), pokemon);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro no banco de dados", badRequestResult.Value);
    }

    #endregion

    #region DeletarPokemon

    [Fact]
    public async Task DeletarPokemon_QuandoValido_DeveRetornar204NoContent()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(pokemon);
        
        var result = await _pokemonController.DeletarPokemon(pokemon.PokemonId);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeletarPokemon_QuandoNaoEncontrado_DeveRetornar404NotFound()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as PokemonResponseDto);
        
        var result = await _pokemonController.DeletarPokemon(pokemon.PokemonId);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeletarPokemon_QuandoErro_DeveRetornar400BadRequest()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var result = await _pokemonController.DeletarPokemon(pokemon.PokemonId);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Erro no banco de dados", badRequestResult.Value);
    }

    #endregion
}