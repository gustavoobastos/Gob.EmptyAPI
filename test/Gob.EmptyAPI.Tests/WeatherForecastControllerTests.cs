using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Gob.EmptyAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Gob.EmptyAPI.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        [Trait(nameof(WeatherForecastController), nameof(WeatherForecastController.Get))]
        public async Task WeatherForecastController_Get_Success()
        {
            // Arrange
            WebApplicationFactory<Program> factory = new();
            HttpClient client = factory.CreateClient();

            // Act
            IEnumerable<WeatherForecast>? response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/WeatherForecast");

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.Equal(5, response.Count());
            Assert.All(response, x =>
            {
                Assert.True(x.Date >= DateTime.Now);
                Assert.True(x.TemperatureC is >= -20 and <= 55);
                Assert.NotNull(x.Summary);
                Assert.NotEmpty(x.Summary);
            });
        }
    }
}
