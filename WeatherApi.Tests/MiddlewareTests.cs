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
using Microsoft.Extensions.Primitives;
using WeatherApi.Logger;
using WeatherApi.Extensions;

namespace WeatherApi.Tests
{
    public class MiddlewareTests
    {
        [Fact]
        public async Task ResponseHeaders_ShouldContainTestHeader()
        {
            var host = await BuildHost();

            var response = await host.GetTestClient().GetAsync("/");

            response.Headers.Contains("test-header").Should().BeTrue();
        }

        [Fact]
        public async Task TestHeader_ShouldHaveCorrectValue()
        {
            var expectedValue = new StringValues("abcdefg");
            var host = await BuildHost();

            var response = await host.GetTestClient().GetAsync("/");

            response.Headers.GetValues("test-header").Should().BeEquivalentTo(expectedValue);
        }

        private async Task<IHost> BuildHost()
        {
            return await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<IHeaderBuilder, HeaderBuilder>();
                            services.AddTransient<IExampleLogger, ExampleLogger>();
                        })
                        .Configure(app =>
                        {
                            app.UseTestHeader();
                        });
                })
                .StartAsync();
        }
    }
}
