using BatalhaDePokemons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatalhaDePokemons.Infra.Mappings;

public class TurnoMapping : IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.ToTable("Turnos");
        builder.HasKey(t => t.TurnoId);
        builder.Property(t => t.TurnoId)
            .HasColumnName(nameof(Turno.TurnoId))
            .ValueGeneratedNever();

        builder.Property(t => t.BatalhaId)
            .HasColumnName(nameof(Batalha.BatalhaId))
            .IsRequired();

        builder.Property(t => t.NumeroTurno)
            .HasColumnName(nameof(Turno.NumeroTurno));
        
        builder.Property(t => t.AtacanteId)
            .HasColumnName(nameof(Turno.AtacanteId))
            .IsRequired();
        
        builder.Property(t=>t.AlvoId)
            .HasColumnName(nameof(Turno.AlvoId))
            .IsRequired();
        
        builder.Property(t=>t.AtaqueUtilizadoId)
            .HasColumnName(nameof(Turno.AtaqueUtilizadoId))
            .IsRequired();

        builder.Property(t => t.DanoCausado)
            .HasColumnName(nameof(Turno.DanoCausado));
        
        builder.Property(t=>t.DataHoraCriacao)
            .HasColumnName(nameof(Turno.DataHoraCriacao));
    }
}