using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        var faculties = new[]
        {
            new Faculty {Id = 1, Name = "Ingeniería"},
            new Faculty {Id = 2, Name = "Ciencias Económicas Administrativas y Contables"},
            new Faculty {Id = 3, Name = "Ciencias Sociales"},
            new Faculty {Id = 4, Name = "Ciencias de la Salud"},
            new Faculty {Id = 5, Name = "Ciencias Jurídicas y Sociales"},
            new Faculty {Id = 6, Name = "Humanidades y Artes"},
            new Faculty {Id = 7, Name = "No Aplica"},
        };
        
        builder.HasData(faculties);
    }
}