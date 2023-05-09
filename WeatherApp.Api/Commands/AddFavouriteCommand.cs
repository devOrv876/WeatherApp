using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Api.Models;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Commands
{
    public record AddFavouriteCommand(double Latitude, double Longitude, string Name): IRequest<FavouriteLocation>;
}
