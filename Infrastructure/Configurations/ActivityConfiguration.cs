using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    private const string ForeignActivityCareersTableName = "ForeignActivityCareers";

    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(activity => activity.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(activity => activity.Name)
            .IsUnique();

        builder.Property(activity => activity.Slug)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(activity => activity.Slug)
            .IsUnique();

        builder.Property(activity => activity.Description)
            .HasMaxLength(350)
            .IsRequired();

        builder.Property(activity => activity.Location)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(activity => activity.ReviewerObservations)
            .HasMaxLength(500);

        builder.Property(activity => activity.MainActivities)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(activity => activity.Goals)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(activity => activity.RequestedBy)
            .WithMany(user => user.RequestedActivities)
            .HasForeignKey(activity => activity.RequestedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(activity => activity.ReviewedBy)
            .WithMany(user => user.ReviewedActivities)
            .HasForeignKey(activity => activity.ReviewedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(activity => activity.Supervisor)
            .WithMany(user => user.SupervisedActivities)
            .HasForeignKey(activity => activity.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(activity => activity.Coordinator)
            .WithMany(user => user.CoordinatedActivities)
            .HasForeignKey(activity => activity.CoordinatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(activity => activity.Scopes)
            .WithOne(user => user.Activity)
            .HasForeignKey(activity => activity.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(activity => activity.Organizers)
            .WithOne(organizer => organizer.Activity)
            .HasForeignKey(activity => activity.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(activity => activity.ForeingCareers)
            .WithMany(career => career.ForeingActivities)
            .UsingEntity(activity => activity.ToTable(ForeignActivityCareersTableName));
    }
}