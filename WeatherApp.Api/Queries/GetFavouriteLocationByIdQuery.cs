using MediatR;
using WeatherApp.Aplication.Models;

namespace WeatherApp.Api.Queries
{
    public record GetFavouriteLocationByIdQuery(Guid Id):IRequest<FavouriteLocation>;
}
