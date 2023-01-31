using Cwiczenia8.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Cwiczenia8.Extensions
{
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
