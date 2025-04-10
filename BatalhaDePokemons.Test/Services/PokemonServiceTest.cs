using BatalhaDePokemons.Domain.Mappers;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Domain.Services;
using BatalhaDePokemons.Test.Domain.Builders;
using Moq;

namespace BatalhaDePokemons.Test.Services;

public class PokemonServiceTest
{
    private readonly PokemonService _pokemonService;
    private readonly Mock<IPokemonRepository> _pokemonRepositoryMock = new();
    private readonly Mock<IAtaqueRepository> _ataqueRepositoryMock = new();
    private readonly Mock<IPokemonAtaqueRepository> _pokemonAtaqueRepositoryMock = new();

    public PokemonServiceTest()
    {
        _pokemonService = new PokemonService(_pokemonRepositoryMock.Object, _ataqueRepositoryMock.Object, _pokemonAtaqueRepositoryMock.Object);
    }

    #region CriarPokemon

    [Fact]
    public async Task CriarPokemon_QuandoValido_DeveRetornarPokemon()
    {
        var pokemon = PokemonBuilder.Novo().Build();

        _pokemonRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Pokemon>())).ReturnsAsync(pokemon.PokemonId);

        var result = await _pokemonService.CriarAsync(PokemonMapper.MapToCreationDto(pokemon));
        Assert.Equal(pokemon.PokemonId, result);
    }

    #endregion

    #region ObterPokemonPorId

    [Fact]
    public async Task ObterPokemonPorId_QuandoValido_DeveRetornarPokemon()
    {
        var pokemon = PokemonBuilder.Novo().Build();

        _pokemonRepositoryMock.Setup(r => r.ObterPorIdComAtaquesAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);

        var result = await _pokemonService.ObterPorIdAsync(Guid.NewGuid());

        Assert.NotNull(result);
        Assert.Equal(pokemon.Nome, result.Nome);
    }

    #endregion

    #region ObterTodosPokemons

    [Fact]
    public async Task ObterTodosPokemons_QuandoValido_DeveRetornarLista()
    {
        var pokemons = new List<Pokemon>
        {
            PokemonBuilder.Novo().Build(),
            PokemonBuilder.Novo().Build()
        };

        _pokemonRepositoryMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(pokemons);

        var result = await _pokemonService.ObterTodosAsync();

        Assert.NotNull(result);
        Assert.Equal(pokemons.Count, result.Count);
    }

    #endregion

    #region AtualizarPokemon

    [Fact]
    public async Task AtualizarPokemon_QuandoValido_DeveRetornarPokemonAtualizado()
    {
        var pokemon = PokemonBuilder.Novo().Build();

        _pokemonRepositoryMock.Setup(r => r.ObterPorIdComAtaquesAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);
        _pokemonRepositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Pokemon>())).ReturnsAsync(pokemon);

        var result = await _pokemonService.AtualizarAsync(Guid.NewGuid(), PokemonMapper.MapToCreationDto(pokemon));

        Assert.Equal(pokemon.Nome, result.Nome);
    }

    #endregion

    #region RemoverPokemon

    [Fact]
    public async Task RemoverPokemon_QuandoValido_DeveRemover()
    {
        var pokemon = PokemonBuilder.Novo().Build();

        _pokemonRepositoryMock.Setup(r => r.ObterPorIdComAtaquesAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);

        await _pokemonService.RemoverAsync(pokemon.PokemonId);
            
        _pokemonRepositoryMock.Verify(r => r.RemoverESalvarAsync(It.IsAny<Pokemon>()), Times.Once);
    }

    #endregion
}