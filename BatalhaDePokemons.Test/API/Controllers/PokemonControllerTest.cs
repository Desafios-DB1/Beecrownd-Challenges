using BatalhaDePokemons.API.Controllers;
using BatalhaDePokemons.Crosscutting.Dtos.Ataque;
using BatalhaDePokemons.Crosscutting.Dtos.Pokemon;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Mappers;
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
            .ReturnsAsync(pokemon.PokemonId);
        
        // Act
        var result = await _pokemonController.CriarPokemon(PokemonMapper.MapToCreationDto(pokemon));
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var returnedGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(pokemon.PokemonId, returnedGuid);
    }

    [Fact]
    public async Task CriarPokemon_QuandoErro_DeveLancarInvalidArgumentException()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s => s.CriarAsync(It.IsAny<PokemonCreationDto>()))
            .ThrowsAsync(new InvalidArgumentException("Tipo inválido"));
        
        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(()=>
            _pokemonController.CriarPokemon(PokemonMapper.MapToCreationDto(pokemon)));
        
        Assert.Equal("Tipo inválido", exception.Message);
    }

    #endregion

    #region VincularAtaque

    [Fact]
    public async Task VincularAtaque_QuandoValido_DeveRetornar200Ok()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        var ataque = AtaqueBuilder.Novo().Build();
        
        _pokemonServiceMock
            .Setup(s=>s.VincularAtaqueAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);
        
        var result = await _pokemonController.VincularAtaque(pokemon.PokemonId, ataque.AtaqueId);
        
        Assert.IsType<OkResult>(result);
        _pokemonServiceMock.Verify(x => x.VincularAtaqueAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task VincularAtaque_QuandoPokemonNaoExiste_DeveLancarNotFoundException()
    {
        _pokemonServiceMock
            .Setup(s=>s.VincularAtaqueAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Pokemon não existe"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _pokemonController.VincularAtaque(Guid.NewGuid(), Guid.NewGuid()));
        
        Assert.Equal("Pokemon não existe", exception.Message);
    }

    #endregion

    #region ObterPokemon

    [Fact]
    public async Task ObterPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PokemonMapper.MapToResponseDto(pokemon));
        
        var result = await _pokemonController.ObterPokemon(pokemon.PokemonId);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedPokemon = Assert.IsType<PokemonResponseDto>(okResult.Value);
        Assert.Equal(pokemon.Nome, returnedPokemon.Nome);
    }

    [Fact]
    public async Task ObterPokemon_QuandoNaoExiste_DeveLancarNotFoundException()
    {
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Pokemon não encontrado"));
        
        var exception = await Assert.ThrowsAsync<NotFoundException>(()=>
            _pokemonController.ObterPokemon(Guid.NewGuid()));
        
        Assert.Equal("Pokemon não encontrado", exception.Message);
    }

    [Fact]
    public async Task ObterPokemon_QuandoErro_DeveLancarException()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var exception = await Assert.ThrowsAsync<Exception>(()=>
            _pokemonController.ObterPokemon(pokemon.PokemonId));
        
        Assert.Equal("Erro no banco de dados", exception.Message);
    }

    #endregion

    #region ObterAtaquesPokemon

    [Fact]
    public async Task ObterAtaquesPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var ataques = new List<AtaqueResponseDto>
        {
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build()),
            AtaqueMapper.MapToResponseDto(AtaqueBuilder.Novo().Build())
        };
        _pokemonServiceMock
            .Setup(s => s.ObterAtaquesPorPokemonIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ataques);
        
        var result = await _pokemonController.ObterAtaquesPokemon(Guid.NewGuid());
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedAtaques = Assert.IsAssignableFrom<List<AtaqueResponseDto>>(okResult.Value);
        Assert.Equal(ataques[0].Nome, returnedAtaques[0].Nome);
    }

    [Fact]
    public async Task ObterAtaquesPokemon_QuandoErro_DeveLancarException()
    {
        _pokemonServiceMock
            .Setup(s=>s.ObterAtaquesPorPokemonIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));
        
        var exception = await Assert.ThrowsAsync<Exception>(()=>
            _pokemonController.ObterAtaquesPokemon(Guid.NewGuid()));
        
        Assert.Equal("Erro no banco de dados", exception.Message);
    }

    #endregion

    #region AtualizarPokemon

    [Fact]
    public async Task AtualizarPokemon_QuandoValido_DeveRetornar200Ok()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ReturnsAsync(PokemonMapper.MapToResponseDto(pokemon));
        
        var result = await _pokemonController.AtualizarPokemon(pokemon.PokemonId, PokemonMapper.MapToCreationDto(pokemon));
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedPokemon = Assert.IsType<PokemonResponseDto>(okResult.Value);
        Assert.Equal(pokemon.Nome, returnedPokemon.Nome);
    }

    [Fact]
    public async Task AtualizarPokemon_QuandoInvalido_DeveLancarInvalidArgumentException()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s => s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ThrowsAsync(new InvalidArgumentException("Tipo inválido"));
        
        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(()=>
            _pokemonController.AtualizarPokemon(Guid.NewGuid(), PokemonMapper.MapToCreationDto(pokemon)));
        
        Assert.Equal("Tipo inválido", exception.Message);
    }

    [Fact]
    public async Task AtualizarPokemon_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s=>s.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<PokemonCreationDto>()))
            .ThrowsAsync(new NotFoundException("Pokemon não encontrado"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(()=>
            _pokemonController.AtualizarPokemon(Guid.NewGuid(), PokemonMapper.MapToCreationDto(pokemon)));
        
        Assert.Equal("Pokemon não encontrado", exception.Message);
    }

    #endregion

    #region DeletarPokemon

    [Fact]
    public async Task DeletarPokemon_QuandoValido_DeveRetornar204NoContent()
    {
        var pokemon = PokemonBuilder.Novo().Build();
        _pokemonServiceMock
            .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PokemonMapper.MapToResponseDto(pokemon));
        
        var result = await _pokemonController.DeletarPokemon(pokemon.PokemonId);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeletarPokemon_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        _pokemonServiceMock
            .Setup(s=>s.RemoverAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Pokemon não encontrado"));
        
        var exception = await Assert.ThrowsAsync<NotFoundException>(()=>
            _pokemonController.DeletarPokemon(Guid.NewGuid()));
        
        Assert.Equal("Pokemon não encontrado", exception.Message);
    }

    [Fact]
    public async Task DeletarPokemon_QuandoErro_DeveLancarException()
    {
        _pokemonServiceMock
            .Setup(s => s.RemoverAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro no banco de dados"));

        var exception = await Assert.ThrowsAsync<Exception>(()=>
            _pokemonController.DeletarPokemon(Guid.NewGuid()));
        
        Assert.Equal("Erro no banco de dados", exception.Message);
    }

    #endregion
}