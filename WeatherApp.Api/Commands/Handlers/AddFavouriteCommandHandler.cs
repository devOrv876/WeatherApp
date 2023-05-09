using MediatR;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;

namespace WeatherApp.Api.Commands.Handlers
{
    public class AddFavouriteCommandHandler : IRequestHandler<AddFavouriteCommand,FavouriteLocation>
    {
        private readonly IWeatherApp _weatherApp;

        public AddFavouriteCommandHandler(IWeatherApp weatherApp)
        {
            _weatherApp = weatherApp;
        }

        public async Task<FavouriteLocation> Handle(AddFavouriteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _weatherApp.AddToFavourites(command.Latitude, command.Longitude, command.Name);



                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
