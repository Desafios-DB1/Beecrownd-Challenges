using BatalhaDePokemons.Application.Dtos;
using BatalhaDePokemons.Application.Dtos.Ataque;
using BatalhaDePokemons.Application.Services;
using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Test.Domain.Builders;
using Moq;

namespace BatalhaDePokemons.Test.Application.Services
{
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

            _ataqueRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Ataque>())).Returns(Task.CompletedTask);

            var result = await _ataqueService.CriarAsync(ataque);

            Assert.NotNull(result);
            Assert.Equal(ataque.Name, result.Name);
        }

        #endregion

        #region ObterPorIdAsync

        [Fact]
        public async Task ObterPorIdAsync_QuandoValido_DeveRetornarAtaque()
        {
            var ataque = AtaqueBuilder.Novo().Build();

            _ataqueRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);

            var result = await _ataqueService.ObterPorIdAsync(ataque.AtaqueId);

            Assert.NotNull(result);
            Assert.Equal(ataque.Name, result.Name);
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

            _ataqueRepositoryMock.Setup(r => r.FindAllAsync()).ReturnsAsync(ataques);

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

            _ataqueRepositoryMock.Setup(r => r.FindByTipoAsync(It.IsAny<Tipo>())).ReturnsAsync(ataques);

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
            _ataqueRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);

            var result = await _ataqueService.AtualizarAsync(Guid.NewGuid(), ataque);

            Assert.NotNull(result);
            Assert.Equal(ataque.Name, result.Name);
        }

        #endregion

        #region RemoverAtaque

        [Fact]
        public async Task RemoverAtaque_QuandoValido_DeveRemover()
        {
            var ataque = AtaqueBuilder.Novo().Build();
            _ataqueRepositoryMock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(ataque);

            await _ataqueService.RemoverAsync(Guid.NewGuid());
            
            _ataqueRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Ataque>()), Times.Once);
        }

        #endregion
    }
}
