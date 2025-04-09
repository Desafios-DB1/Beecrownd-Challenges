using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Dtos.Batalha;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Batalha;
using BatalhaDePokemons.Crosscutting.Exceptions.Pokemon;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Domain.Mappers;
using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Domain.Repositories;

namespace BatalhaDePokemons.Domain.Services;

public class BatalhaService 
    (IPokemonRepository pokemonRepository, 
    IBatalhaRepository batalhaRepository, 
    ITurnoRepository turnoRepository) : IBatalhaService
{
    public async Task<Guid> IniciarBatalha(Guid atacanteId, Guid defensorId)
    {
        var (pokemon1, pokemon2) = await ValidarPokemons(atacanteId, defensorId);

        if (pokemon1.IsDesmaiado || pokemon2.IsDesmaiado)
            throw new PokemonsDevemEstarSaudaveisException(ExceptionMessages.PokemonsDevemEstarSaudaveis);

        var emBatalha = await batalhaRepository.ExisteBatalhaAtivaComPokemonAsync(pokemon1.PokemonId, pokemon2.PokemonId);
        if (emBatalha)
            throw new PokemonsEmBatalhaException(ExceptionMessages.PokemonsJaBatalhando);

        var batalha = new Batalha
        {
            BatalhaId = Guid.NewGuid(),
            Pokemon1Id = atacanteId,
            Pokemon2Id = defensorId,
            IsFinalizada = false,
            ProximoTurnoDoPokemonId = atacanteId,
            Turnos = []
        };

        await batalhaRepository.AdicionarESalvarAsync(batalha);
        return batalha.BatalhaId;
    }

    public async Task ExecutarTurno(Guid batalhaId, Guid atacanteId, Guid ataqueId)
    {
        var batalha = await ValidarBatalha(batalhaId);
        if (batalha.ProximoTurnoDoPokemonId != atacanteId)
            throw new AtacanteInvalidoException(ExceptionMessages.PokemonAtacandoForaDoTurno);
        
        var (ataque, defensor) = await ObterAtaqueEAlvoAsync(batalha, atacanteId, ataqueId);
        
        defensor.RecebeDano(ataque.Poder);

        if (defensor.IsDesmaiado)
        {
            batalha.VencedorId = atacanteId;
            batalha.IsFinalizada = true;
            batalha.ProximoTurnoDoPokemonId = Guid.Empty;
        }
        else
        {
            batalha.ProximoTurnoDoPokemonId = defensor.PokemonId;
        }

        var turno = new Turno
        {
            BatalhaId = batalhaId,
            NumeroTurno = batalha.Turnos.Count + 1,
            AtacanteId = atacanteId,
            AlvoId = defensor.PokemonId,
            AtaqueUtilizadoId = ataqueId,
            DanoCausado = ataque.Poder
        };
        
        await turnoRepository.AdicionarESalvarAsync(turno);
        await pokemonRepository.AtualizarESalvarAsync(defensor);
        await batalhaRepository.AtualizarESalvarAsync(batalha);
    }

    public async Task EncerrarBatalhaAsync(Guid batalhaId, Guid desistenteId)
    {
        var batalha = await ValidarBatalha(batalhaId);

        if (batalha.Pokemon1Id != desistenteId && batalha.Pokemon2Id != desistenteId)
            throw new PokemonNaoParticipaException(ExceptionMessages.PokemonNaoParticipaDaBatalha);
        
        var vencedorId = batalha.Pokemon1Id == desistenteId 
            ? batalha.Pokemon2Id
            : batalha.Pokemon1Id;

        batalha.IsFinalizada = true;
        batalha.VencedorId = vencedorId;

        await batalhaRepository.AtualizarESalvarAsync(batalha);
    }

    public async Task<BatalhaDetalhadaDto> ObterBatalhaDetalhadaAsync(Guid batalhaId)
    {
        var batalha = await batalhaRepository.ObterPorIdComTurnosAsync(batalhaId)
                      ?? throw new NotFoundException(ExceptionMessages.BatalhaNaoEncontrada(batalhaId));
        
        var (pokemon1, pokemon2) = await ValidarPokemons(batalha.Pokemon1Id, batalha.Pokemon2Id);
        
        return BatalhaMapper.MapToDetalhadaDto(batalha, pokemon1, pokemon2);
    }

    public async Task<List<BatalhaResponseDto>> ObterTodasBatalhas()
    {
        var batalhas = await batalhaRepository.ObterTodosComTurnosAsync();
        return BatalhaMapper.MapToResponseDtos(batalhas);
    }

    private async Task<Batalha> ValidarBatalha(Guid batalhaId)
    {
        var batalha = await batalhaRepository.ObterPorIdComTurnosAsync(batalhaId)
            ?? throw new NotFoundException(ExceptionMessages.BatalhaNaoEncontrada(batalhaId));

        if (batalha.IsFinalizada)
            throw new BatalhaFinalizadaException(ExceptionMessages.BatalhaJaFinalizada);
        
        return batalha;
    }

    private async Task<(Ataque ataque, Pokemon defensor)> ObterAtaqueEAlvoAsync(Batalha batalha, Guid atacanteId, Guid ataqueId)
    {
        var defensorId = batalha.Pokemon1Id == atacanteId ? batalha.Pokemon2Id : batalha.Pokemon1Id;
        var (atacante, defensor) = await ValidarPokemons(atacanteId, defensorId);

        var ataque = ValidarAtaque(atacante, ataqueId);
        
        return (ataque, defensor);
    }

    private static Ataque ValidarAtaque(Pokemon atacante, Guid ataqueId)
    {
        return atacante.Ataques.FirstOrDefault(a => a.AtaqueId == ataqueId)
               ?? throw new AtaqueInvalidoException(ExceptionMessages.PokemonNaoConheceAtaque);
    }

    private async Task<(Pokemon pokemon1, Pokemon pokemon2)> ValidarPokemons(Guid pokemon1Id, Guid pokemon2Id)
    {
        var pokemon1 = await pokemonRepository.ObterPorIdComAtaquesAsync(pokemon1Id)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(pokemon1Id));
        
        var pokemon2 = await pokemonRepository.ObterPorIdComAtaquesAsync(pokemon2Id)
            ?? throw new NotFoundException(ExceptionMessages.PokemonNaoEncontrado(pokemon2Id));
        
        return (pokemon1, pokemon2);
    }
}