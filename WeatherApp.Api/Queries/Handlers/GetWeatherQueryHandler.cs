using MediatR;
using WeatherApp.Aplication.Models;
using WeatherApp.Application.Interfaces;

namespace WeatherApp.Api.Queries.Handlers
{
    public class GetWeatherQueryHandler: IRequestHandler<GetWeatherQuery, WeatherModel>
    {
        private readonly IWeatherApp _weatherApp;

        public GetWeatherQueryHandler(IWeatherApp weatherApp)
        {
            _weatherApp = weatherApp;
        }

        public async Task<WeatherModel> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _weatherApp.GetWeatherAsync(request.Latitude, request.Longitude);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
