﻿using BatalhaDePokemons.Domain.Models;
using BatalhaDePokemons.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BatalhaDePokemons.Infra;

public class PokemonsDbContext(DbContextOptions<PokemonsDbContext> options) : DbContext(options)
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Ataque> Ataques { get; set; }
    public DbSet<PokemonAtaque> PokemonAtaques { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PokemonMapping());
        modelBuilder.ApplyConfiguration(new AtaqueMapping());
        modelBuilder.ApplyConfiguration(new PokemonAtaqueMapping());
    }
}