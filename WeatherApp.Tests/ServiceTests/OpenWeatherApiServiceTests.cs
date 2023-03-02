using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherApp.Api.HttpClients.Interfaces;
using WeatherApp.Api.Models;
using WeatherApp.Api.Services;

namespace WeatherApp.Tests.ServiceTests
{
    public class OpenWeatherApiServiceTests
    {
        public OpenWeatherApiServiceTests() { }

        [Fact]
        public async Task GetWeatherByLocationAsync_ReturnsWeatherModel()
        {
            //arrange
            var logger = Mock.Of<ILogger<OpenWeatherApiService>>();
            var mockWeatherClient = new Mock<IOpenWeatherHttpClient>();
            mockWeatherClient.Setup(s => s.GetWeatherForLocationAsync(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(
                new OpenWeatherResponseModel
                {
                    lat = 51.51f,
                    lon = -0.13f,
                    timezone = "London",
                    current = new Current
                    {
                        dt = 1593100000,
                        temp = 15.5f,
                        feels_like = 15.5f,
                        pressure = 1016,
                        sunrise = 4058,
                        sunset = 1708,
                        humidity = 93,
                        dew_point = 14.44f,
                        uvi = 0,
                        clouds = 0,
                        visibility = 10000,
                        wind_speed = 3.6f,
                        wind_deg = 240,
                        weather = new List<Weather>
                        {
                            new Weather
                            {
                                id = 800,
                                main = "Clear",
                                description = "clear sky",
                                icon = "01d"
                            }
                        }.ToArray()
                    },
                    daily = new List<Daily>()
                    {
                        new Daily()
                        {
                            temp = new Temp()
                            {
                                max = 18.1f,
                                min = 12.2f
                            }
                        }
                    }.ToArray()

                });


            var service = new OpenWeatherApiService(mockWeatherClient.Object, logger);

            // Act
            var result = await service.GetWeatherAsync(51.5074, 0.1278);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(15.5f, result.Temperature.Current);
            Assert.Equal("London", result.LocationName);
            Assert.Equal(93, result.Humidity);
            Assert.Equal(1016, result.Pressure);
            Assert.Equal(4058, result.Sunrise);
            Assert.Equal(1708, result.Sunset);
        }


        [Fact]
        public async Task GetWeatherByLocationAsync_ThrowsException()
        {
            //arrange
            var logger = Mock.Of<ILogger<OpenWeatherApiService>>();
            var mockWeatherClient = new Mock<IOpenWeatherHttpClient>();
            mockWeatherClient.Setup(s => s.GetWeatherForLocationAsync(It.IsAny<double>(), It.IsAny<double>())).ThrowsAsync(new Exception("Test Exception"));
            var service = new OpenWeatherApiService(mockWeatherClient.Object, logger);

            // Act
            Func<Task> act = () => service.GetWeatherAsync(51.5074, 0.1278);

            // Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains("Test Exception", ex.Message);
        }
    }
}
