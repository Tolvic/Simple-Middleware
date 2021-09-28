using System;
using System.Threading.Tasks;

namespace WeatherApi.Logger
{
    public class ExampleLogger : IExampleLogger
    {
        public Task LogError()
        {
            throw new NotImplementedException();
        }
    }
}
