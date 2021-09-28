using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WeatherApi.Builder;
using WeatherApi.Logger;

namespace WeatherApi.Middleware
{
    public class TestHeaderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHeaderBuilder headerBuilder;
        private readonly IExampleLogger logger;

        public TestHeaderMiddleware(RequestDelegate next, IHeaderBuilder headerBuilder, IExampleLogger logger)
        {
            this.next = next;
            this.headerBuilder = headerBuilder;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch
            {
                _ = Task.Run(logger.LogError);
            }

            var header = headerBuilder.BuildTestHeader();

            httpContext.Response.Headers.Add(header);

        }
    }
}
