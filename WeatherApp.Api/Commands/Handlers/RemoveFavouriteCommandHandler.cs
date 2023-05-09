using MediatR;

namespace WeatherApp.Api.Commands.Handlers
{
    public class RemoveFavouriteCommandHandler : IRequestHandler<RemoveFavouriteCommand, bool>
    {
        public Task<bool> Handle(RemoveFavouriteCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
