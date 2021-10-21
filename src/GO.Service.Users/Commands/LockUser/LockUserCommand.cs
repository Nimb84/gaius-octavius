using MediatR;

namespace GO.Service.Users.Commands.LockUser
{
    public sealed record LockUserCommand(Guid UserId, Guid CurrentUserId)
        : IRequest<Unit>;
}
