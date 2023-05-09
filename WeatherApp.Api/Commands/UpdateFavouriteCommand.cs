using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Commands
{
    public record UpdateFavouriteCommand(Guid Id, double Latitude, double Longitude, string Name): IRequest<FavouriteLocation>;
}
