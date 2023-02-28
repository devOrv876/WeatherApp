using WeatherApp.Api.Models;

namespace WeatherApp.Api.HttpClients
{
    public interface IOpenWeatherHttpClient
    {
        Task<OpenWeatherResponseModel> GetWeatherForLocationAsync(string latitude, string longitude);
    }
}
