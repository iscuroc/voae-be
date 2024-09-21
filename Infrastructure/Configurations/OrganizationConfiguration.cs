using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(x => x.Activities)
            .WithOne(a => a.Organization)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        var organizations = new[]
        {
            new Organization {Id = 1, Name = "Pumas en Acción"},
            new Organization {Id = 2, Name = "Pumas Solidarios"},
            new Organization {Id = 3, Name = "Estudiantina"},
            new Organization {Id = 4, Name = "VOAE"},
            new Organization {Id = 5, Name = "Admisiones"},
            new Organization {Id = 6, Name = "Salud VOAE"},
            new Organization {Id = 7, Name = "Humanidades, Artes y Deporte"},
            new Organization {Id = 8, Name = "Área de Matemáticas"},
            new Organization {Id = 9, Name = "Área de Biología y Química"},
            new Organization {Id = 10, Name = "Área de Inglés"}
        };
        
        builder.HasData(organizations);
    }
}