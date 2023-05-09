using MediatR;

namespace WeatherApp.Api.Commands
{
    public record RemoveFavouriteCommand(Guid Id): IRequest<bool>;
}