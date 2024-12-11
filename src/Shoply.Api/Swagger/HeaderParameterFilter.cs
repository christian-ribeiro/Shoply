using Microsoft.OpenApi.Models;
using Shoply.Arguments.DataAnnotation;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Shoply.Api.Swagger;

public class HeaderParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var headerIgnore = context.MethodInfo.GetCustomAttribute<HeaderIgnoreAttribute>(true);
        if (headerIgnore != null)
            return;

        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "RETURN-PROPERTY",
            In = ParameterLocation.Header,
            Required = false,
            Description = "Utilization: [\"Property1\", \"Property2\", \"NavigationProperty.Property1\", \"NavigationProperty.Property2\"]",
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}