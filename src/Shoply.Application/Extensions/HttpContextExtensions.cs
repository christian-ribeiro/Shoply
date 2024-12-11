using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Shoply.Application.Extensions;

public static class HttpContextExtensions
{
    public static TType? GetHeader<TType>(this HttpContext httpContext, string key)
    {
        if (httpContext.Request.Headers.TryGetValue(key, out StringValues value) && value.Count > 0)
        {
            try
            {
                string stringValue = value.ToString();

                if (typeof(TType) == typeof(string))
                    return (TType)(object)stringValue;

                return (TType)Convert.ChangeType(stringValue, typeof(TType));
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Erro ao converter o header '{key}' para o tipo {typeof(TType).Name}: {ex.Message}");
            }
        }

        return default;
    }
}