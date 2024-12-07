using Microsoft.EntityFrameworkCore;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.Mapping.Module.Registration;

namespace Shoply.Infrastructure.Persistence.Context;

public class ShoplyDbContext(DbContextOptions<ShoplyDbContext> options) : DbContext(options)
{
    #region Registration
    public DbSet<User>? User { get; set; }
    public DbSet<Customer>? Customer { get; set; }
    public DbSet<CustomerAddress>? CustomerAddress { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
    }
}