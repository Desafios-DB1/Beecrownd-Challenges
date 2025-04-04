using BatalhaDePokemons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatalhaDePokemons.Infra.Mappings;

public class PokemonAtaqueMapping : IEntityTypeConfiguration<PokemonAtaque>
{
    public void Configure(EntityTypeBuilder<PokemonAtaque> builder)
    {
        builder.ToTable("PokemonAtaque");
        builder.HasKey(pa => new { pa.PokemonId, pa.AtaqueId });
        builder.Property(pa => pa.PokemonId)
            .HasColumnName("PokemonId");
        
        builder.Property(pa => pa.AtaqueId)
            .HasColumnName("AtaqueId");
        
        builder.HasOne(pa => pa.Pokemon)
            .WithMany(p => p.PokemonAtaques)
            .HasForeignKey(pa => pa.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pa => pa.Ataque)
            .WithMany(a => a.PokemonAtaques)
            .HasForeignKey(pa => pa.AtaqueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}