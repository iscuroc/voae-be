using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(350)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.MainActivities)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Objectives)
            .HasMaxLength(300)
            .IsRequired();

        /*builder.Property(x => x.CareerId)
            .IsRequired();
        */

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.TotalSpots)
            .IsRequired();

        /*builder.Property(x => x.SupervisorId)
            .IsRequired();
        */

        /*builder.Property(x => x.RequestedById)
            .IsRequired();
        */

        builder.Property(x => x.RequestDate)
            .IsRequired();
    }
}
