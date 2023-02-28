using WeatherApp.Api.Models;

public interface IOpenWeatherApiService
{
    Task<WeatherModel> GetWeatherAsync(double latitude, double longitude);

}