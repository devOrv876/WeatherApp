using WeatherApp.Aplication.Models;

namespace WeatherApp.Application.Services.Interfaces
{
    public interface IOpenWeatherApiService
    {
        Task<WeatherModel> GetWeatherAsync(double latitude, double longitude);

    }

}
