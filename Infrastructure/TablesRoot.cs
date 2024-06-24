using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public abstract class TablesRoot(DbContextOptions options) : DbContext(options)
{
    public required DbSet<User> Users { get; set; }
}