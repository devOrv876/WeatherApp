using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Api.Models;
using WeatherApp.Api.Queries;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public WeatherAppController( IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WeatherModel), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { nameof(WeatherRequestModel.Latitude), nameof(WeatherRequestModel.Longitude) })]
        public async Task<IActionResult> Get([FromQuery] WeatherRequestModel request)
        {
            var result = await _mediatR.Send(new GetWeatherQuery(request.Longitude, request.Latitude));
            return Ok(result);
        }

    }
}
