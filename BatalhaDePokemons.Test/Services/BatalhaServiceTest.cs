﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Batalha;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Batalha;
using BatalhaDePokemons.Crosscutting.Exceptions.Pokemon;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Domain.Services;
using BatalhaDePokemons.Test.Domain.Builders;
using Moq;
using Xunit;

namespace BatalhaDePokemons.Test.Services
{
    public class BatalhaServiceTest
    {
        private readonly BatalhaService _batalhaService;
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock = new();
        private readonly Mock<IBatalhaRepository> _batalhaRepositoryMock = new();
        private readonly Mock<ITurnoRepository> _turnoRepositoryMock = new();

        public BatalhaServiceTest()
        {
            _batalhaService = new BatalhaService(
                _pokemonRepositoryMock.Object,
                _batalhaRepositoryMock.Object,
                _turnoRepositoryMock.Object);
        }

        [Fact]
        public async Task IniciarBatalha_QuandoValido_DeveCriarBatalha()
        {
            var atacante = PokemonBuilder.Novo().Build();
            var defensor = PokemonBuilder.Novo().Build();
            var batalha = BatalhaBuilder.Novo().Build();

            _pokemonRepositoryMock.Setup(r => r.FindByIdWithAtaquesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => atacante)
                .Callback<Guid>(id =>
                {
                    if (id == defensor.PokemonId)
                        _pokemonRepositoryMock.Setup(r => r.FindByIdWithAtaquesAsync(id)).ReturnsAsync(defensor);
                });

            _batalhaRepositoryMock.Setup(r => r.ExisteBatalhaAtivaComPokemonAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            _batalhaRepositoryMock.Setup(r => r.AddAndCommitAsync(It.IsAny<Batalha>())).ReturnsAsync(batalha.BatalhaId);

            var result = await _batalhaService.IniciarBatalha(atacante.PokemonId, defensor.PokemonId);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task IniciarBatalha_QuandoPokemonDesmaiado_DeveLancarExcecao()
        {
            var atacante = PokemonBuilder.Novo().Build();
            var defensor = PokemonBuilder.Novo().ComDesmaiado().Build();

            _pokemonRepositoryMock.SetupSequence(r => r.FindByIdWithAtaquesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(atacante)
                .ReturnsAsync(defensor);

            await Assert.ThrowsAsync<PokemonsDevemEstarSaudaveisException>(() =>
                _batalhaService.IniciarBatalha(atacante.PokemonId, defensor.PokemonId));
        }

        [Fact]
        public async Task ExecutarTurno_QuandoValido_DeveExecutarComSucesso()
        {
            var atacante = PokemonBuilder.Novo().ComAtaques().Build();
            var defensor = PokemonBuilder.Novo().ComAtaques().Build();
            var ataque = atacante.Ataques.First();

            var batalha = new Batalha
            {
                BatalhaId = Guid.NewGuid(),
                Pokemon1Id = atacante.PokemonId,
                Pokemon2Id = defensor.PokemonId,
                IsFinalizada = false,
                ProximoTurnoDoPokemonId = atacante.PokemonId,
                Turnos = []
            };

            _batalhaRepositoryMock.Setup(r => r.FindByIdWithTurnos(batalha.BatalhaId)).ReturnsAsync(batalha);
            _pokemonRepositoryMock.SetupSequence(r => r.FindByIdWithAtaquesAsync(It.IsAny<Guid>()))
                .ReturnsAsync(atacante)
                .ReturnsAsync(defensor);

            await _batalhaService.ExecutarTurno(batalha.BatalhaId, atacante.PokemonId, ataque.AtaqueId);

            _turnoRepositoryMock.Verify(r => r.AddAndCommitAsync(It.IsAny<Turno>()), Times.Once);
            _pokemonRepositoryMock.Verify(r => r.UpdateAndCommitAsync(defensor), Times.Once);
            _batalhaRepositoryMock.Verify(r => r.UpdateAndCommitAsync(batalha), Times.Once);
        }

        [Fact]
        public async Task EncerrarBatalha_QuandoValido_DeveEncerrar()
        {
            var pokemon1 = PokemonBuilder.Novo().Build();
            var pokemon2 = PokemonBuilder.Novo().Build();

            var batalha = new Batalha
            {
                BatalhaId = Guid.NewGuid(),
                Pokemon1Id = pokemon1.PokemonId,
                Pokemon2Id = pokemon2.PokemonId,
                IsFinalizada = false
            };

            _batalhaRepositoryMock.Setup(r => r.FindByIdWithTurnos(batalha.BatalhaId)).ReturnsAsync(batalha);

            await _batalhaService.EncerrarBatalhaAsync(batalha.BatalhaId, pokemon1.PokemonId);

            _batalhaRepositoryMock.Verify(r => r.UpdateAndCommitAsync(It.Is<Batalha>(b => b.IsFinalizada)), Times.Once);
        }

        [Fact]
        public async Task ObterBatalhaDetalhada_QuandoNaoExiste_DeveLancarNotFound()
        {
            _batalhaRepositoryMock.Setup(r => r.FindByIdWithTurnos(It.IsAny<Guid>())).ReturnsAsync((Batalha)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _batalhaService.ObterBatalhaDetalhadaAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task ObterTodasBatalhas_DeveRetornarLista()
        {
            _batalhaRepositoryMock.Setup(r => r.FindAllWithTurnosAsync()).ReturnsAsync(new List<Batalha>());

            var result = await _batalhaService.ObterTodasBatalhas();

            Assert.NotNull(result);
        }
    }
}
