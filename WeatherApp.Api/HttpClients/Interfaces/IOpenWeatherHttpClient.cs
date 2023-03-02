using WeatherApp.Api.Models;

namespace WeatherApp.Api.HttpClients.Interfaces
{
    public interface IOpenWeatherHttpClient
    {
        Task<OpenWeatherResponseModel> GetWeatherForLocationAsync(double latitude, double longitude);
    }
}
