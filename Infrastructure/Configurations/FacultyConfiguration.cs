using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
