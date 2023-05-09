using MediatR;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Queries
{
    public record GetWeatherQuery(double Latitude, double Longitude): IRequest<WeatherModel>;
}
