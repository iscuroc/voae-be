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
            new Organization {Id = 3, Name = "VOAE"},
            new Organization {Id = 4, Name = "Estudiantina"}
        };

        builder.HasData(organizations);
    }
}