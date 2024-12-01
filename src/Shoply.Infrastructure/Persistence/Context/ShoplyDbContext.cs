using Microsoft.EntityFrameworkCore;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.Mapping.Module.Registration;

namespace Shoply.Infrastructure.Persistence.Context;

public class ShoplyDbContext(DbContextOptions<ShoplyDbContext> options) : DbContext(options)
{
    public DbSet<User>? User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
    }
}