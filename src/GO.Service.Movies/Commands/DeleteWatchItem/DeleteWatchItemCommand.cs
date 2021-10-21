using MediatR;

namespace GO.Service.Movies.Commands.DeleteWatchItem
{
    public sealed record DeleteWatchItemCommand(Guid UserId, Guid MovieId)
        : IRequest<Unit>;
}
