using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Names)
            .HasMaxLength(50);
        
        builder.Property(x => x.Lastname)
            .HasMaxLength(50);
        
        builder.Property(x => x.Email)
            .HasMaxLength(255);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.HasIndex(x => x.AccountNumber)
            .IsUnique();
        
    }
}