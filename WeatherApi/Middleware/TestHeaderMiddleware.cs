using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WeatherApi.Builder;

namespace WeatherApi.Middleware
{
    public class TestHeaderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHeaderBuilder headerBuilder;

        public TestHeaderMiddleware(RequestDelegate next, IHeaderBuilder headerBuilder)
        {
            this.next = next;
            this.headerBuilder = headerBuilder;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var header = headerBuilder.BuildTestHeader();

            httpContext.Response.Headers.Add(header);

            await next(httpContext);
        }
    }
}
