using Shoply.Application.Argument.Authentication;
using Shoply.Application.Argument.Integration;

namespace Shoply.Api.Extensions;

public static class ConfigureExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("SMTP"));
        services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
        return services;
    }
}