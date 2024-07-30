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

        /*builder.Property(x => x.SupervisorId)
            .IsRequired();
        */

        /*builder.Property(x => x.RequestedById)
            .IsRequired();
        */

        //relation one to many with scopes, one activity can have many scopes
        builder.HasMany(x => x.Scopes)
            .WithOne(x => x.Activity)
            .HasForeignKey(x => x.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);
        //relation many to many with careers, one activity can have many careers
        builder.HasMany(x => x.ForeingCareers)
            .WithMany(x => x.ForaingActivities)
            .UsingEntity(x => x.ToTable("ActivityCareers"));
        //relation one to many with activity, one career can have many activities
        builder.HasOne(x => x.MainCareer)
            .WithMany(x => x.MainActivities)
            .HasForeignKey(x => x.MainCareerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}