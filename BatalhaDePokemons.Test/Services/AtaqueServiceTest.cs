using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Mappers;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Domain.Services;
using BatalhaDePokemons.Test.Domain.Builders;
using Moq;

namespace BatalhaDePokemons.Test.Services;

public class AtaqueServiceTest
{
    private readonly AtaqueService _ataqueService;
    private readonly Mock<IAtaqueRepository> _ataqueRepositoryMock = new();

    public AtaqueServiceTest()
    {
        _ataqueService = new AtaqueService(_ataqueRepositoryMock.Object);
    }

    #region CriarAsync

    [Fact]
    public async Task CriarAsync_QuandoValido_DeveRetornarAtaque()
    {
        var ataque = AtaqueBuilder.Novo().Build();

        _ataqueRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Ataque>())).ReturnsAsync(ataque.AtaqueId);

        var result = await _ataqueService.CriarAsync(AtaqueMapper.MapToCreationDto(ataque));
        Assert.Equal(ataque.AtaqueId, result);
    }

    #endregion

    #region ObterPorIdAsync

    [Fact]
    public async Task ObterPorIdAsync_QuandoValido_DeveRetornarAtaque()
    {
        var ataque = AtaqueBuilder.Novo().Build();

        _ataqueRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);

        var result = await _ataqueService.ObterPorIdAsync(ataque.AtaqueId);

        Assert.NotNull(result);
        Assert.Equal(ataque.Nome, result.Nome);
    }

    #endregion

    #region ObterTodosAsync

    [Fact]
    public async Task ObterTodosAsync_QuandoValido_DeveRetornarListDeAtaques()
    {
        var ataques = new List<Ataque>
        {
            AtaqueBuilder.Novo().Build(),
            AtaqueBuilder.Novo().Build()
        };

        _ataqueRepositoryMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(ataques);

        var result = await _ataqueService.ObterTodosAsync();

        Assert.NotNull(result);
        Assert.Equal(ataques.Count, result.Count);
    }

    #endregion

    #region ObterTodosPorTipoAsync

    [Fact]
    public async Task ObterTodosPorTipoAsync_QuandoValido_DeveRetornarLista()
    {
        var ataques = new List<Ataque>
        {
            AtaqueBuilder.Novo().ComTipo(Tipo.Fogo).Build()
        };

        _ataqueRepositoryMock.Setup(r => r.ObterPorTipoAsync(It.IsAny<Tipo>())).ReturnsAsync(ataques);

        var result = await _ataqueService.ObterPorTipoAsync(Tipo.Fogo);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    #endregion

    #region AtualizarAtaque

    [Fact]
    public async Task AtualizarAtaque_QuandoValido_DeveRetornarAtaqueAtualizado()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);
        _ataqueRepositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Ataque>())).ReturnsAsync(ataque);

        var result = await _ataqueService.AtualizarAsync(ataque.AtaqueId, AtaqueMapper.MapToCreationDto(ataque));
            
        Assert.Equal(result.Nome, ataque.Nome);
    }

    #endregion

    #region RemoverAtaque

    [Fact]
    public async Task RemoverAtaque_QuandoValido_DeveRemover()
    {
        var ataque = AtaqueBuilder.Novo().Build();
        _ataqueRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);

        await _ataqueService.RemoverAsync(Guid.NewGuid());
            
        _ataqueRepositoryMock.Verify(r => r.RemoverESalvarAsync(It.IsAny<Ataque>()), Times.Once);
    }

    #endregion
}