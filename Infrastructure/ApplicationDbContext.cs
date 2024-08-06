using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : TablesRoot(options)
{
    public DbSet<UserCareerResponse> UserCareerResponses { get; set; }
    public DbSet<UserResponse> UserResponses { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries<IEntityBase>())
        {
            if (entry.State is EntityState.Detached or EntityState.Unchanged or EntityState.Deleted)
                continue;
            
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}