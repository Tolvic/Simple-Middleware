using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WeatherApi.Middleware
{
    public class TestHeader
    {
        private readonly RequestDelegate next;

        public TestHeader(RequestDelegate next)
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
