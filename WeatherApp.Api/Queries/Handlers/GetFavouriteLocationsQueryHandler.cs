using MediatR;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;

namespace WeatherApp.Api.Queries.Handlers
{
    public class GetFavouriteLocationsQueryHandler : IRequestHandler<GetFavouriteLocationsQuery, IEnumerable<FavouriteLocation>>
    {
        private readonly IWeatherApp _weatherApp;

        public GetFavouriteLocationsQueryHandler(IWeatherApp weatherApp)
        {
            _weatherApp = weatherApp;
        }

        public async Task<IEnumerable<FavouriteLocation>> Handle(GetFavouriteLocationsQuery request, CancellationToken cancellationToken)
        {
            var result = await _weatherApp.GetFavouriteLocationsAync();

        
            return result;
        }
    }
}
