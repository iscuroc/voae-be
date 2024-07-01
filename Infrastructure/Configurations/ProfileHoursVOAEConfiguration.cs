using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProfileHoursVOAEConfiguration : IEntityTypeConfiguration<ProfileHoursVOAE>
{
    public void Configure(EntityTypeBuilder<ProfileHoursVOAE> builder)
    {
        builder.Property(x => x.Role)
            .IsRequired();
    }
}
