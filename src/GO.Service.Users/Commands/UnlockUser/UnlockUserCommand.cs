using MediatR;

namespace GO.Service.Users.Commands.UnlockUser
{
    public sealed record UnlockUserCommand(Guid UserId, Guid CurrentUserId)
        : IRequest<Unit>;
}
