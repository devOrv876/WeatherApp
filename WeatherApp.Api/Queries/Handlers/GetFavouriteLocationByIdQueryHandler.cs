using MediatR;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;

namespace WeatherApp.Api.Queries.Handlers
{
    public class GetFavouriteLocationByIdQueryHandler : IRequestHandler<GetFavouriteLocationByIdQuery, FavouriteLocation>
    {
        private readonly IWeatherApp _weatherApp;

        public GetFavouriteLocationByIdQueryHandler(IWeatherApp weatherApp)
        {
            _weatherApp = weatherApp;
        }
        public async Task<FavouriteLocation> Handle(GetFavouriteLocationByIdQuery request, CancellationToken cancellationToken) 
        {
			try
			{
				var result = await _weatherApp.GetFavouriteLocationById(request.Id);
                return result;
            }
			catch (Exception ex)
			{

				throw;
			}
        }
    }
}
