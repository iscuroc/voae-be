using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserResponseConfiguration : IEntityTypeConfiguration<UserResponse>
    {
        public void Configure(EntityTypeBuilder<UserResponse> builder)
        {
            builder.HasKey(ur => ur.Id);
            
            builder.Property(ur => ur.Names)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ur => ur.Lastnames)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ur => ur.AccountNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ur => ur.Role)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ur => ur.Career)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
