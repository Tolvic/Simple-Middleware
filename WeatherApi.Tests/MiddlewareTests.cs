using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using WeatherApi.Builder;
using Xunit;
using WeatherApi.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using FluentAssertions;

namespace WeatherApi.Tests
{
    public class MiddlewareTests
    {
        [Fact]
        public async Task ResponseHeaders_ShouldContainTestHeader()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<IHeaderBuilder, HeaderBuilder>();
                        })
                        .Configure(app =>
                            {
                                app.UseMiddleware<TestHeaderMiddleware>();
                            });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync("/");

            response.Headers.Contains("test-header").Should().BeTrue();
        }
    }
}
