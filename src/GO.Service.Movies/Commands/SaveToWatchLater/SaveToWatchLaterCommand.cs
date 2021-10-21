using MediatR;

namespace GO.Service.Movies.Commands.SaveToWatchLater
{
    public sealed record SaveToWatchLaterCommand(Guid UserId, Guid MovieId)
        : IRequest<Unit>;
}
