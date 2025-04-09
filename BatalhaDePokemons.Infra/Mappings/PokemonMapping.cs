using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatalhaDePokemons.Infra.Mappings;

public class PokemonMapping : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.ToTable("Pokemons");
        builder.HasKey(p => p.PokemonId);
        builder.Property(p => p.PokemonId)
            .HasColumnName(nameof(Pokemon.PokemonId))
            .ValueGeneratedNever();
        
        builder.Property(p => p.Nome)
            .HasColumnName(nameof(Pokemon.Nome))
            .HasMaxLength(Caracteres.Duzentos)
            .IsRequired();

        builder.Property(p => p.Nivel)
            .HasColumnName(nameof(Pokemon.Nivel))
            .HasDefaultValue(1);

        builder.Property(p=>p.IsDesmaiado)
            .HasColumnName(nameof(Pokemon.IsDesmaiado))
            .HasDefaultValue(false);
        
        builder.Property(p=>p.DataHoraCriacao)
            .HasColumnName(nameof(Pokemon.DataHoraCriacao));
        
        builder.Property(p => p.Tipo)
            .HasColumnName(nameof(Pokemon.Tipo))
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<Tipo>(v))
            .IsRequired();
        
        builder.OwnsOne(p => p.Status, status =>
        {
            status.Property(s => s.PontosDeVida)
                .HasColumnName("Hp")
                .IsRequired();
            
            status.Property(s=>s.Velocidade)
                .HasColumnName("Spd")
                .IsRequired();
            
            status.Property(s=>s.Defesa)
                .HasColumnName("Def")
                .IsRequired();
            
            status.Property(s => s.Ataque)
                .HasColumnName("Atk")
                .IsRequired();
        });

        builder.HasMany(p => p.Ataques)
            .WithMany(a => a.Pokemons)
            .UsingEntity<PokemonAtaque>(
                l =>
                    l.HasOne(pa => pa.Ataque)
                        .WithMany(a => a.PokemonAtaques)
                        .HasForeignKey(pa => pa.AtaqueId),
                r =>
                    r.HasOne(pa => pa.Pokemon)
                        .WithMany()
                        .HasForeignKey(pa => pa.PokemonId),

                j =>
                {
                    j.ToTable("PokemonsAtaques");
                    j.HasKey(pa => new { pa.PokemonId, pa.AtaqueId });
                }
            );
    }
}