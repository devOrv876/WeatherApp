using WeatherApp.Api.Models;
using WeatherApp.Api.Services.Interfaces;
using WeatherApp.Infrastructure.Interfaces;

namespace WeatherApp.Api.Services
{
    public class FavouriteWeatherLocationsService : IFavouriteWeatherLocationsService
    {
        private readonly IFavouriteLocationsRepository _favouriteLocationsRepository;
        private readonly ILogger<FavouriteWeatherLocationsService> _logger;

        public FavouriteWeatherLocationsService(IFavouriteLocationsRepository favouriteLocationsRepository, ILogger<FavouriteWeatherLocationsService> logger)
        {
            _favouriteLocationsRepository = favouriteLocationsRepository;
            _logger = logger;
        }

        
        public async Task<FavouriteLocation> AddToFavouritesAsync(double latitude, double longititude, string city)
        {
            try
            {
                var favourite = await _favouriteLocationsRepository.AddFavouriteAsync(latitude, longititude, city);
                return new FavouriteLocation() { Id = favourite.Id, Latitude = favourite.Latitude, Longitude = favourite.Longitude, LocationName = favourite.LocationName };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<FavouriteLocation> GetFavouriteLocationByIdAsync(Guid id)
        {
            try
            {
                var favourite = await _favouriteLocationsRepository.GetFavouriteByIdAsync(id);
                if (favourite == null)
                {
                    return null;
                }
                return new FavouriteLocation() { Id = favourite.Id, Latitude = favourite.Latitude, Longitude = favourite.Longitude, LocationName = favourite.LocationName };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<FavouriteLocation>> GetFavouriteLocationsAsync()
        {
            try
            {
                var favourites = await _favouriteLocationsRepository.GetAllFavouritesAsync();
                return favourites.Select(f => new FavouriteLocation() { Id = f.Id, Latitude = f.Latitude, Longitude = f.Longitude, LocationName = f.LocationName }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<bool> RemoveFromFavouritesAync(Guid id)
        {
            try
            {
                var favourite = await _favouriteLocationsRepository.GetFavouriteByIdAsync(id);
                return favourite != null ? await _favouriteLocationsRepository.DeleteFavouriteAsync(favourite) : false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<FavouriteLocation> UpdateFavouriteAsync(Guid id, double latitude, double longititude, string name)
        {
            try
            {
                var updatedFavourite = await _favouriteLocationsRepository.UpdateFavouriteAsync(id, latitude, longititude, name);
                return new FavouriteLocation() { Id = updatedFavourite.Id, Latitude = updatedFavourite.Latitude, Longitude = updatedFavourite.Longitude, LocationName = updatedFavourite.LocationName };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
