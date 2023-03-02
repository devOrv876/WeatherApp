using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Api.Commands;
using WeatherApp.Api.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteWeatherController : ControllerBase
    {
        private readonly IFavouriteWeatherLocationsService _favouriteWeatherLocationsService;

        public FavouriteWeatherController(IFavouriteWeatherLocationsService favouriteWeatherLocationsService)
        {
            _favouriteWeatherLocationsService = favouriteWeatherLocationsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FavouriteLocation>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFavouriteLocations()
        {
            return Ok(await _favouriteWeatherLocationsService.GetFavouriteLocationsAsync());
        }
        
        [HttpGet("{id}",Name = nameof(GetFavouriteLocationById))]
        [ProducesResponseType(typeof(FavouriteLocation), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFavouriteLocationById(Guid id)
        {
            var result = await _favouriteWeatherLocationsService.GetFavouriteLocationByIdAsync(id);
            if (result == null)
            {
                return NotFound(new
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Favourite location not found"
                });
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FavouriteLocation), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddToFavourites([FromBody] AddFavouriteCommand command)
        {
            var result = await _favouriteWeatherLocationsService.AddToFavouritesAsync(command.Latitude, command.Longitude, command.Name);
            return CreatedAtRoute(nameof(GetFavouriteLocationById), new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateFavourite([FromBody] UpdateFavouriteCommand command)
        {
            var existingLocation = await _favouriteWeatherLocationsService.GetFavouriteLocationByIdAsync(command.Id);
            if (existingLocation == null)
            {
                return NotFound(new
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Favourite location not found"
                });
            }
            var result = await _favouriteWeatherLocationsService.UpdateFavouriteAsync(command.Id, command.Latitude, command.Longitude, command.Name);
            return CreatedAtAction(nameof(GetFavouriteLocationById), new { id = result.Id }, result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveFromFavourites([FromQuery] Guid id)
        {
            var existingLocation = await _favouriteWeatherLocationsService.GetFavouriteLocationByIdAsync(id);
            if (existingLocation == null)
            {
                return NotFound(new
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Favourite location not found"
                });
            }
            await _favouriteWeatherLocationsService.RemoveFromFavouritesAync(id);
            return NoContent();
        }
    }
}
