using System.Threading.Tasks;
using WeatherApi.Builder;
using Xunit;
using WeatherApi.Middleware;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;
using System.Collections.Generic;
using System;
using WeatherApi.Logger;

namespace WeatherApi.Tests
{
    public class MiddlewareUnitTests
    {
        private Mock<IHeaderBuilder> _headerBuilder;
        private Mock<IExampleLogger> _logger;

        public MiddlewareUnitTests()
        {
            _headerBuilder = new Mock<IHeaderBuilder>();

            _headerBuilder.Setup(x => x.BuildTestHeader()).Returns(new KeyValuePair<string, StringValues>("test-header", "abcdefg"));

            _logger = new Mock<IExampleLogger>();
        }

        [Fact]
        public async Task TestHeaderMiddleware_ShouldCallHeaderBuilder()
        {
            // Arrange
            var httpContext = GetHttpContext();
            var middlewareInstance = new TestHeaderMiddleware(GetNextRequestDelegate(), _headerBuilder.Object, _logger.Object);

            // Act
            await middlewareInstance.Invoke(httpContext);

            // Assert
            _headerBuilder.Verify( x => x.BuildTestHeader(), Times.Once);
        }

        [Fact]
        public async Task TestHeaderMiddleware_ShouldCalErrorLogger_WhenNextRequestDelegateThrowsAnException()
        {
            // Arrange
            var httpContext = GetHttpContext();

            var middlewareInstance = new TestHeaderMiddleware(
                (innerHttpContext) => { throw new Exception(); },
                _headerBuilder.Object,
                _logger.Object);

            // Act
            await middlewareInstance.Invoke(httpContext);

            // Assert
            _logger.Verify(x => x.LogError(), Times.Once);
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
