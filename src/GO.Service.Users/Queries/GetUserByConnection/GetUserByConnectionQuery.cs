using GO.Service.Users.Enums;
using GO.Service.Users.Models;
using MediatR;

namespace GO.Service.Users.Queries.GetUserByConnection
{
    public sealed record GetUserByConnectionQuery(string ConnectionId, ConnectionType ConnectionType)
        : IRequest<UserResponse>;
}
