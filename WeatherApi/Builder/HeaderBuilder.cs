using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace WeatherApi.Builder
{
    public class HeaderBuilder : IHeaderBuilder
    {
        public KeyValuePair<string, StringValues> BuildTestHeader()
        {
            return new KeyValuePair<string, StringValues>("test-header", "abcdefg");
        }
    }
}
