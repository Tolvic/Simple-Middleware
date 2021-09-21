using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace WeatherApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseTestHeader(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var header = new KeyValuePair<string, StringValues>("test-header", "abcdefg");

                context.Response.Headers.Add(header);

                await next();

            });
        }
    }
}
