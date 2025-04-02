using BatalhaDePokemons.Crosscutting.Constantes;
using BatalhaDePokemons.Domain.Enums;
using BatalhaDePokemons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatalhaDePokemons.Infra.Mappings;

public class AtaqueMapping : IEntityTypeConfiguration<Ataque>
{
    public void Configure(EntityTypeBuilder<Ataque> builder)
    {
        builder.ToTable("Ataques");
        builder.HasKey(a => a.AtaqueId);
        builder.Property(a => a.AtaqueId)
            .HasColumnName(nameof(Ataque.AtaqueId))
            .ValueGeneratedNever();
        
        builder.Property(a => a.Name)
            .HasMaxLength(Caracteres.Duzentos)
            .IsRequired();
        
        builder.Property(a => a.Tipo)
            .HasColumnName(nameof(Ataque.Tipo))
            .HasConversion(
                v=>v.ToString(),
                v=> Enum.Parse<Tipo>(v))
            .IsRequired();

        builder.Property(a => a.Poder)
            .HasColumnName(nameof(Ataque.Poder))
            .IsRequired();

        builder.Property(a => a.Precisao)
            .HasColumnName(nameof(Ataque.Precisao))
            .HasDefaultValue(Caracteres.Cinquenta);

        builder.Property(a => a.PP)
            .HasColumnName(nameof(Ataque.PP))
            .HasDefaultValue(Caracteres.Cinco);
    }
}