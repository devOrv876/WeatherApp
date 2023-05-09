using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Api.Models
{
    public record UpdateFavouriteModel(Guid Id, double Latitude, double Longitude, string Name);
}
