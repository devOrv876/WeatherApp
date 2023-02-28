using Microsoft.AspNetCore.Http;
using WeatherApp.Api.HttpClients;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.Services
{
    public class OpenWeatherApiService: IOpenWeatherApiService
    {
        private readonly IOpenWeatherHttpClient _openWeatherHttpClient;

        public OpenWeatherApiService(IOpenWeatherHttpClient openWeatherHttpClient)
        {
           _openWeatherHttpClient = openWeatherHttpClient;
        }

        public async Task<WeatherModel> GetWeatherAsync(double latitude, double longitude)
        {
            var results = await _openWeatherHttpClient.GetWeatherForLocationAsync("", "");
            var weatherData = MapWeatherData(results);
            return weatherData;
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
