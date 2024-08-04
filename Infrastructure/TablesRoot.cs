using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public abstract class TablesRoot(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Career> Careers => Set<Career>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<ActivityOrganizer> ActivityOrganizers => Set<ActivityOrganizer>();
    public DbSet<ActivityScope> ActivityScopes => Set<ActivityScope>();
    public DbSet<Organization> Organizations => Set<Organization>();
}