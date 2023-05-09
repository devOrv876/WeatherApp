using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Application.Interfaces
{
    public interface IWeatherApp
    {
        Task<WeatherModel> GetWeatherAsync(double latitude, double longitude);

        Task<IEnumerable<FavouriteLocation>> GetFavouriteLocationsAync();

        Task<FavouriteLocation> GetFavouriteLocationById(Guid id);

        Task<FavouriteLocation> AddToFavourites(double latitude, double longitude, string name);

        Task<FavouriteLocation> UpdateFavourite(Guid id, double latitude, double longitude, string name);

        Task<bool> RemoveFromFavourites(Guid id);
    }
}
