﻿

using Microsoft.Extensions.Logging;
using WeatherApp.Aplication.Models;
using WeatherApp.Applcation.HttpClients.Interfaces;
using WeatherApp.Application.Services.Interfaces;

namespace WeatherApp.Application.Services
{
    public class OpenWeatherApiService: IOpenWeatherApiService
    {
        private readonly IOpenWeatherHttpClient _openWeatherHttpClient;
        private readonly ILogger<OpenWeatherApiService> _logger;

        public OpenWeatherApiService(IOpenWeatherHttpClient openWeatherHttpClient, ILogger<OpenWeatherApiService> logger)
        {
           _openWeatherHttpClient = openWeatherHttpClient;
            _logger = logger;
        }

        public async Task<WeatherModel> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                var results = await _openWeatherHttpClient.GetWeatherForLocationAsync(latitude, longitude);
                var weatherData = MapWeatherData(results);
                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private WeatherModel MapWeatherData(OpenWeatherResponseModel model)
        {
            var weatherData = new WeatherModel();
            weatherData.Temperature.Current = model.current.temp;
            weatherData.Temperature.Maximum = model.daily[0].temp.max;
            weatherData.Temperature.Minimum = model.daily[0].temp.min;
            weatherData.LocationName = model.timezone;
            weatherData.Humidity = model.current.humidity;
            weatherData.Pressure = model.current.pressure;
            weatherData.Sunrise = model.current.sunrise;
            weatherData.Sunset = model.current.sunset;

            return weatherData;
        }
    }
}
