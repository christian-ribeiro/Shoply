using Lamar.Microsoft.DependencyInjection;

namespace Shoply.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static ConfigureHostBuilder ConfigureDependencyInjection(this ConfigureHostBuilder host)
    {
        host.UseLamar((context, registry) =>
        {
            registry.Scan(scanner =>
            {
                scanner.Assembly("Shoply.Domain");
                scanner.Assembly("Shoply.Infrastructure");
                scanner.Assembly("Shoply.Application");
                scanner.WithDefaultConventions();
            });
        });

        return host;
    }
}
