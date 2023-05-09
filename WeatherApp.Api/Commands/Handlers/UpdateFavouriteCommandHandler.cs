using MediatR;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;

namespace WeatherApp.Api.Commands.Handlers
{
    public class UpdateFavouriteCommandHandler : IRequestHandler<UpdateFavouriteCommand,FavouriteLocation>
    {
        private readonly IWeatherApp _weatherApp;

        public UpdateFavouriteCommandHandler(IWeatherApp weatherApp)
        {
            _weatherApp = weatherApp;
        }
        public async Task<FavouriteLocation> Handle(UpdateFavouriteCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var result = await _weatherApp.UpdateFavourite(request.Id,request.Latitude, request.Longitude, request.Name);

                return result;
            }
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
