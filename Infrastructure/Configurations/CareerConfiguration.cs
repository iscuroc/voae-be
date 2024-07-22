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
        
        var careers = new[]
        {
            new Career {Id = 1, Name = "Ingeniería en Sistemas", FacultyId = 1},
            new Career {Id = 2, Name = "Ingeniería Agroindustrial", FacultyId = 2},
            new Career {Id = 3, Name = "Licenciatura en Comercio Internacional", FacultyId = 2},
            new Career {Id = 4, Name = "Licenciatura en Desarrollo Local", FacultyId = 3},
            new Career {Id = 5, Name = "Licenciatura en Administración de Empresas", FacultyId = 2},
            new Career {Id = 6, Name = "Técnico en Administración de Empresas Cafetaleras", FacultyId = 2},
            new Career {Id = 7, Name = "Técnico en Producción Agrícola", FacultyId = 1}
        };
        
        builder.HasData(careers);
    }
}