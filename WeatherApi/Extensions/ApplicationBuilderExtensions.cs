using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
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
