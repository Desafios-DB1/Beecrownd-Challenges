using BatalhaDePokemons.Application.Services;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Test.Domain.Builders;
using Moq;

namespace BatalhaDePokemons.Test.Application.Services
{
    public class PokemonServiceTest
    {
        private readonly PokemonService _pokemonService;
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock = new();

        public PokemonServiceTest()
        {
            _pokemonService = new PokemonService(_pokemonRepositoryMock.Object, null);
        }

        #region CriarPokemon

        [Fact]
        public async Task CriarPokemon_QuandoValido_DeveRetornarPokemon()
        {
            var pokemon = PokemonBuilder.Novo().Build();

            _pokemonRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Pokemon>())).Returns(Task.CompletedTask);

            var result = await _pokemonService.CriarAsync(pokemon);

            Assert.NotNull(result);
            Assert.Equal(pokemon.Name, result.Name);
        }

        #endregion

        #region ObterPokemonPorId

        [Fact]
        public async Task ObterPokemonPorId_QuandoValido_DeveRetornarPokemon()
        {
            var pokemon = PokemonBuilder.Novo().Build();

            _pokemonRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);

            var result = await _pokemonService.ObterPorIdAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(pokemon.Name, result.Name);
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

            _pokemonRepositoryMock.Setup(r => r.FindAllAsync()).ReturnsAsync(pokemons);

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

            _pokemonRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);
            _pokemonRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Pokemon>())).Returns(Task.CompletedTask);

            var result = await _pokemonService.AtualizarAsync(Guid.NewGuid(), pokemon);

            Assert.NotNull(result);
            Assert.Equal(pokemon.Name, result.Name);
        }

        #endregion

        #region RemoverPokemon

        [Fact]
        public async Task RemoverPokemon_QuandoValido_DeveRemover()
        {
            var pokemon = PokemonBuilder.Novo().Build();

            _pokemonRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pokemon);

            await _pokemonService.RemoverAsync(pokemon.PokemonId);
            
            _pokemonRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Pokemon>()), Times.Once);
        }

        #endregion
    }
}