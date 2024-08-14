using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ActivityMemberConfiguration: IEntityTypeConfiguration<ActivityMember>
{
    public void Configure(EntityTypeBuilder<ActivityMember> builder)
    {
        builder.Property(am => am.ActivityId)
            .IsRequired();

        builder.Property(am => am.MemberId)
            .IsRequired();

        builder.HasOne(am => am.Activity)
            .WithMany(a => a.Members)
            .HasForeignKey(am => am.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(am => am.Member)
            .WithMany(u => u.JoinedActivities)
            .HasForeignKey(am => am.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(am => am.Scopes)
            .WithOne(ams => ams.ActivityMember)
            .HasForeignKey(ams => ams.ActivityMemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}