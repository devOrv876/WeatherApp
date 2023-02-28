using WeatherApp.Api.Services.Interfaces;

namespace WeatherApp.Api.Services
{
    public class FavouriteWeatherLocationsService : IFavouriteWeatherLocationsService
    {
        public Task AddToFavourites(string latitude, string longititude, string city)
        {
            throw new NotImplementedException();
        }

        public Task<List<FavouriteLocation>> GetFavouriteLocations()
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromFavourites(string latitude, string longititude, string city)
        {
            throw new NotImplementedException();
        }
    }
}
