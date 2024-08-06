using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserCareerResponseConfiguration : IEntityTypeConfiguration<UserCareerResponse>
    {
        public void Configure(EntityTypeBuilder<UserCareerResponse> builder)
        {
            builder.HasKey(uc => uc.Id);
            
            builder.Property(uc => uc.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
