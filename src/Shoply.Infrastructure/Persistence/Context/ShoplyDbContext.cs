using Microsoft.EntityFrameworkCore;

namespace Shoply.Infrastructure.Persistence.Context;

public class ShoplyDbContext(DbContextOptions<ShoplyDbContext> options) : DbContext(options) { }