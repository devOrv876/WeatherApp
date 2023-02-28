using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Api.Services.Interfaces;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {
        private readonly IOpenWeatherApiService _weatherService;
        private readonly IFavouriteWeatherLocationsService _favouriteWeatherLocationsService;

        public WeatherAppController(IOpenWeatherApiService weatherService, IFavouriteWeatherLocationsService favouriteWeatherLocationsService)
        {
            _weatherService = weatherService;
            _favouriteWeatherLocationsService = favouriteWeatherLocationsService;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
