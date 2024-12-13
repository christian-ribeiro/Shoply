using Shoply.Application.Argument.Authentication;
using Shoply.Application.Argument.Integration;
using Shoply.Translation.Argument.Translation;

namespace Shoply.Api.Extensions;

public static class ConfigureExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("SMTP"));
        services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
        services.Configure<TranslateMongoConfiguration>(builder.Configuration.GetSection("Translate:Mongo"));
        services.Configure<TranslateRedisConfiguration>(builder.Configuration.GetSection("Translate:Redis"));
        return services;
    }
}