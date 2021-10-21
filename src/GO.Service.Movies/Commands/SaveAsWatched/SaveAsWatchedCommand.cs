using MediatR;

namespace GO.Service.Movies.Commands.SaveAsWatched
{
    public sealed record SaveAsWatchedCommand(Guid UserId, Guid MovieId)
        : IRequest<Unit>;
}
