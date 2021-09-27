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
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;
using System.Collections.Generic;

namespace WeatherApi.Tests
{
    public class MiddlewareUnitTests
    {
        private Mock<IHeaderBuilder> _headerBuilder;

        public MiddlewareUnitTests()
        {
            _headerBuilder = new Mock<IHeaderBuilder>();

            _headerBuilder.Setup(x => x.BuildTestHeader()).Returns(new KeyValuePair<string, StringValues>("test-header", "abcdefg"));
        }

        [Fact]
        public async Task ResponseHeaders_ShoulCallHeaderBuilder()
        {
            // Arrange
            var httpContext = GetHttpContext();
            var middlewareInstance = new TestHeaderMiddleware(GetNextRequestDelegate(), _headerBuilder.Object);

            // Act
            await middlewareInstance.Invoke(httpContext);

            // Assert
            _headerBuilder.Verify(); 
        }

        private static DefaultHttpContext GetHttpContext()
        {
            DefaultHttpContext defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";
            return defaultContext;
        }

        private static RequestDelegate GetNextRequestDelegate()
        {
            return (innerHttpContext) =>
            {
                return Task.CompletedTask;
            };
        }
    }
}
