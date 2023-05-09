using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;
using WeatherApp.Application.Services;
using WeatherApp.Application.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WeatherApp.Application
{
    public class WeatherApplication : IWeatherApp
    {
        private readonly IOpenWeatherApiService _openWeatherApiService;
        private readonly IFavouriteWeatherLocationsService _favouriteWeatherLocationsService;
        private readonly ILogger<WeatherApplication> _logger;

        public WeatherApplication(IOpenWeatherApiService openWeatherApiService, IFavouriteWeatherLocationsService favouriteWeatherLocationsService, ILogger<WeatherApplication> logger)
        {
            _openWeatherApiService = openWeatherApiService;
            _favouriteWeatherLocationsService = favouriteWeatherLocationsService;
            _logger = logger;
        }

        public async Task<WeatherModel> GetWeatherAsync(double latitude,double longitude)
        {
            var result = await _openWeatherApiService.GetWeatherAsync(latitude, longitude);
            return result;
        }

        public async Task<IEnumerable<FavouriteLocation>> GetFavouriteLocationsAync() 
        {
            var result = await _favouriteWeatherLocationsService.GetFavouriteLocationsAsync();

            return result;
        }

        public async Task<FavouriteLocation> AddToFavourites(double latitude,double longitude, string name)
        {
            var result = await _favouriteWeatherLocationsService.AddToFavouritesAsync(latitude, longitude, name);

            return result;
        }

        public async Task<FavouriteLocation> GetFavouriteLocationById(Guid id)
        {
            var location = await _favouriteWeatherLocationsService.GetFavouriteLocationByIdAsync(id);
            var weather = await  _openWeatherApiService.GetWeatherAsync(location.Latitude, location.Longitude);
            location.Temperature = weather.Temperature;
            location.Humidity = weather.Humidity;
            location.Pressure = weather.Pressure;
            location.Sunrise = weather.Sunrise;
            location.Sunset = weather.Sunset;

            return location;
        }

        public async Task<bool> RemoveFromFavourites(Guid id) 
        {
            var result = await _favouriteWeatherLocationsService.RemoveFromFavouritesAync(id);
            return result;
        }

        public async Task<FavouriteLocation> UpdateFavourite(Guid id, double latitude, double longitude, string name) 
        {
            var result = await _favouriteWeatherLocationsService.UpdateFavouriteAsync(id,latitude, longitude, name);

            return result;
        }
    }
}
