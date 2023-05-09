using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.Api.Commands;
using WeatherApp.Api.Models;
using WeatherApp.Api.Queries;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteWeatherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FavouriteWeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FavouriteLocation>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFavouriteLocations()
        {
            return Ok(await _mediator.Send(new GetFavouriteLocationsQuery()));
        }
        
        [HttpGet("{id}",Name = nameof(GetFavouriteLocationById))]
        [ProducesResponseType(typeof(FavouriteLocation), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFavouriteLocationById(Guid id)
        {
            var result = await _mediator.Send(new GetFavouriteLocationByIdQuery(id));
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
        public async Task<IActionResult> AddToFavourites([FromBody] AddFavouriteModel model)
        {
            var result = await _mediator.Send(new AddFavouriteCommand(model.Latitude,model.Longitude,model.Name));
            return CreatedAtRoute(nameof(GetFavouriteLocationById), new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateFavourite([FromBody] UpdateFavouriteModel model)
        {
            var result = await _mediator.Send(new UpdateFavouriteCommand(model.Id, model.Latitude, model.Longitude, model.Name));
            if (result == null)
            {
                return NotFound(new
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Favourite location not found"
                });
            }
            //var result = await _favouriteWeatherLocationsService.UpdateFavouriteAsync(command.Id, command.Latitude, command.Longitude, command.Name);
            return CreatedAtAction(nameof(GetFavouriteLocationById), new { id = result.Id }, result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveFromFavourites([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new RemoveFavouriteCommand(id));

            if (!result)
            {
                return BadRequest(new
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Message = "Favourite location not found"
                });
            }
            return NoContent();
        }
    }
}
