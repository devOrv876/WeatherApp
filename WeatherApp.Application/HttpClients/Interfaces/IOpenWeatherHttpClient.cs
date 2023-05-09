using WeatherApp.Aplication.Models;

namespace WeatherApp.Applcation.HttpClients.Interfaces
{
    public interface IOpenWeatherHttpClient
    {
        Task<OpenWeatherResponseModel> GetWeatherForLocationAsync(double latitude, double longitude);
    }
}
