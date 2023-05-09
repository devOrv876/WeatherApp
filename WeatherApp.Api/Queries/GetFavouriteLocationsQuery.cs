using MediatR;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Queries
{
    public record GetFavouriteLocationsQuery:IRequest<IEnumerable<FavouriteLocation>>;
}
