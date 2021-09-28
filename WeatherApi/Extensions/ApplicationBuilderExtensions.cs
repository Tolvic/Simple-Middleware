using Microsoft.AspNetCore.Builder;
using WeatherApi.Middleware;

namespace WeatherApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTestHeader(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestHeaderMiddleware>();
        }
    }
}
