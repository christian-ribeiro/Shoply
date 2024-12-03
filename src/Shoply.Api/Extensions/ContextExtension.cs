using Microsoft.EntityFrameworkCore;
using Shoply.Infrastructure.Persistence.Context;

namespace Shoply.Api.Extensions;

public static class ContextExtension
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services)
    {
        services.AddDbContext<ShoplyDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}