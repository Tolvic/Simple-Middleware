using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApi.Middleware
{
    public class TestHeaderMiddleware
    {
        private readonly RequestDelegate next;

        public TestHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // do something
            await next(httpContext);
        }
    }
}
