namespace Shoply.Api.Extensions;

public static class CorsExtension
{
    private static string _corsPolicy = "Shoply";

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(_corsPolicy, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        return services;
    }

    public static WebApplication ApplyCors(this WebApplication app)
    {
        app.UseCors(_corsPolicy);
        return app;
    }
}