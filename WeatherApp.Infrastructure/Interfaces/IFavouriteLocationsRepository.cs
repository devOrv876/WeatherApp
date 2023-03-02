using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure.Models;

namespace WeatherApp.Infrastructure.Interfaces
{
    public interface IFavouriteLocationsRepository
    {
        Task<IEnumerable<FavouriteLocationDto>> GetAllFavouritesAsync();
        Task<FavouriteLocationDto> GetFavouriteByIdAsync(Guid id);
        Task<FavouriteLocationDto> AddFavouriteAsync(double lat, double @long, string name);
        Task<FavouriteLocationDto> UpdateFavouriteAsync(Guid id, double lat, double @long, string name);
        Task<bool> DeleteFavouriteAsync(FavouriteLocationDto favouriteLocation);
            
    }
}
