using GO.Service.Users.Enums;
using GO.Service.Users.Models;
using MediatR;

namespace GO.Service.Users.Queries.GetUser
{
    public sealed record GetUserQuery(
            Guid UserId,
            ConnectionType? ConnectionType)
        : IRequest<UserResponse>;
}
