using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure.Interfaces;
using WeatherApp.Infrastructure.Models;

namespace WeatherApp.Infrastructure.Repository
{
    public class FavouriteLocationsRepository : IFavouriteLocationsRepository
    {
        private readonly WeatherAppDBContext _context;
        private readonly ILogger<FavouriteLocationsRepository> _logger;

        public FavouriteLocationsRepository(WeatherAppDBContext context, ILogger<FavouriteLocationsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<FavouriteLocationDto> AddFavouriteAsync(double lat, double @long, string name)
        {
            try
            {
                var result = _context.FavouriteLocations.Add(new FavouriteLocationDto()
                {
                    Latitude = lat,
                    Longitude = @long,
                    LocationName = name
                });

                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task<FavouriteLocationDto> UpdateFavouriteAsync(Guid id, double lat, double @long, string name)
        {
            try
            {
                var result = _context.FavouriteLocations.Update(new FavouriteLocationDto()
                {
                    Id = id,
                    Latitude = lat,
                    Longitude = @long,
                    LocationName = name
                });

                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task<bool> DeleteFavouriteAsync(FavouriteLocationDto location)
        {
            try
            {
                var result = _context.Remove(location);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task<IEnumerable<FavouriteLocationDto>> GetAllFavouritesAsync()
        {
            try
            {
                return await _context.FavouriteLocations.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task<FavouriteLocationDto> GetFavouriteByIdAsync(Guid id)
        {
            try
            {
                return await _context.FavouriteLocations.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

    }
}
