using Shoply.Application.Argument.Authentication;
using Shoply.Application.Argument.AWS.S3;
using Shoply.Application.Argument.Integration;
using Shoply.Arguments.Argument.Base;
using Shoply.Translation.Argument.Translation;

namespace Shoply.Api.Extensions;

public static class ConfigureExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<FeatureFlags>(builder.Configuration.GetSection("FeatureFlags"));
        services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("Smtp"));
        services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
        services.Configure<TranslateMongoConfiguration>(builder.Configuration.GetSection("Translate:Mongo"));
        services.Configure<TranslateRedisConfiguration>(builder.Configuration.GetSection("Translate:Redis"));
        services.Configure<S3Configuration>(builder.Configuration.GetSection("AWS:S3"));
        return services;
    }
}