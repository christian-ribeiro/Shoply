using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Shoply.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            const string BearerScheme = "Bearer";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shoply API",
                    Description = "API de demonstração de conceito",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Christian Ribeiro",
                        Url = new Uri("https://github.com/christian-ribeiro")
                    }
                });

                c.AddSecurityDefinition(BearerScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = BearerScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = BearerScheme
                            }
                        },
                        new List<string>()
                    }
                });

                c.TagActionsBy(api =>
                {
                    var actionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    return [actionDescriptor!.ControllerName];
                });
            });

            return services;
        }
        public static WebApplication ApplySwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoply API v1");
                c.DocumentTitle = "Shoply API Documentation";
                c.EnableFilter();
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
            });
            return app;
        }
    }
}
