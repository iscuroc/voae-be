using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CareerConfiguration : IEntityTypeConfiguration<Career>
{
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.Faculty)
            .WithMany(c => c.Careers)
            .HasForeignKey(x => x.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Activities)
            .WithOne(a => a.Career)
            .HasForeignKey(x => x.CareerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        var careers = new[]
        {
            new Career {Id = 1, Name = "Ingeniería en Sistemas", FacultyId = 1},
            new Career {Id = 2, Name = "Ingeniería Agroindustrial", FacultyId = 2},
            new Career {Id = 3, Name = "Licenciatura en Comercio Internacional con Orientación en Agroindustria", FacultyId = 2},
            new Career {Id = 4, Name = "Desarrollo Local", FacultyId = 3},
            new Career {Id = 5, Name = "Licenciatura en Administración de Empresas Agropecuarias", FacultyId = 2},
            new Career {Id = 6, Name = "Técnico en Administración de Empresas Cafetaleras", FacultyId = 2},
            new Career {Id = 7, Name = "Técnico en Producción Agrícola", FacultyId = 1},
            new Career {Id = 8, Name = "Licenciatura en Administración y Generación de Empresas", FacultyId = 1},
            new Career {Id = 9, Name = "Técnico en Microfinanzas", FacultyId = 1},
            new Career {Id = 10, Name = "Técnico en Administración de Empresas Agropecuiarias", FacultyId = 2},
            new Career {Id = 11, Name = "Pedagogía", FacultyId = 6},
            new Career {Id = 12, Name = "No Aplica", FacultyId = 7}

        };
        
        builder.HasData(careers);
    }
}