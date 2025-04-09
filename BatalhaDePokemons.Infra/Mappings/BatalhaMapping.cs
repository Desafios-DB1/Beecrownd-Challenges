using BatalhaDePokemons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatalhaDePokemons.Infra.Mappings;

public class BatalhaMapping : IEntityTypeConfiguration<Batalha>
{
    public void Configure(EntityTypeBuilder<Batalha> builder)
    {
        builder.ToTable("Batalhas");
        builder.HasKey(b => b.BatalhaId);
        builder.Property(b => b.BatalhaId)
            .HasColumnName("BatalhaId")
            .ValueGeneratedNever();
        
        builder.Property(b=>b.Pokemon1Id)
            .HasColumnName(nameof(Batalha.Pokemon1Id))
            .IsRequired();
        
        builder.Property(b=>b.Pokemon2Id)
            .HasColumnName(nameof(Batalha.Pokemon2Id))
            .IsRequired();

        builder.Property(b => b.VencedorId)
            .HasColumnName(nameof(Batalha.VencedorId));
        
        builder.Property(b=>b.IsFinalizada)
            .HasColumnName(nameof(Batalha.IsFinalizada));
        
        builder.Property(b=>b.ProximoTurnoDoPokemonId)
            .HasColumnName(nameof(Batalha.ProximoTurnoDoPokemonId));

        builder.HasMany(b => b.Turnos)
            .WithOne(t => t.Batalha)
            .HasForeignKey(t => t.BatalhaId);
    }
}