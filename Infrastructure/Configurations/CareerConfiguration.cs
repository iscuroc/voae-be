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
    }
}
