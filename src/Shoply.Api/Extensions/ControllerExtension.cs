using System.Text.Json.Serialization;

namespace Shoply.Api.Extensions;

public static class ControllerExtension
{
    public static IServiceCollection ConfigureController(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

        services.AddEndpointsApiExplorer();
        return services;
    }

    public static WebApplication ApplyController(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}