using Microsoft.EntityFrameworkCore;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Mapping.Module.Registration;

namespace Shoply.Infrastructure.Persistence.EFCore.Context;

public class ShoplyDbContext(DbContextOptions<ShoplyDbContext> options) : DbContext(options)
{
    public DbSet<User>? User { get; set; }
    public DbSet<Customer>? Customer { get; set; }
    public DbSet<CustomerAddress>? CustomerAddress { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
    }
}