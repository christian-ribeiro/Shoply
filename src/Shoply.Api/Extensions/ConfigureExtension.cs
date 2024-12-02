using Shoply.Application.Integration.Argument.Service;

namespace Shoply.Api.Extensions;

public static class ConfigureExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("SMTP"));
        return services;
    }
}