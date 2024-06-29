using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public abstract class TablesRoot(DbContextOptions options) : DbContext(options)
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<Status> Statuses { get; set; }
    public required DbSet<Career> Careers {get; set;}
    public required DbSet<ActivitiesHours> ActivitiesHours { get; set; }

}