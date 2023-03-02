using WeatherApp.Api.Models;

namespace WeatherApp.Api.Services.Interfaces
{
    public class FavouriteLocation : WeatherModel
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}