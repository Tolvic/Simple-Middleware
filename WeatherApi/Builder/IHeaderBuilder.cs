using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace WeatherApi.Builder
{
    public interface IHeaderBuilder
    {
        KeyValuePair<string, StringValues> BuildTestHeader();
    }
}