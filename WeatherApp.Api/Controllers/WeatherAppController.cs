using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Api.Models;
using WeatherApp.Api.Services.Interfaces;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {
        private readonly IOpenWeatherApiService _weatherService;

        public WeatherAppController(IOpenWeatherApiService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WeatherModel), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { nameof(WeatherRequestModel.Latitude), nameof(WeatherRequestModel.Longitude) })]
        public async Task<IActionResult> Get([FromQuery] WeatherRequestModel request)
        {
            var result = await _weatherService.GetWeatherAsync(request.Longitude, request.Latitude);
            return Ok(result);
        }

    }
}
