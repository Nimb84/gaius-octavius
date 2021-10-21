using GO.HostBuilder.Exceptions;
using GO.Service.Users.Entities;
using GO.Service.Users.Helpers;
using GO.Service.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users.Queries.GetUserByConnection
{
    internal sealed class GetUserByConnectionHandler
        : IRequestHandler<GetUserByConnectionQuery, UserResponse>
    {
        private readonly UserDbContext _context;

        public GetUserByConnectionHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(
            GetUserByConnectionQuery request,
            CancellationToken cancellationToken)
        {
            var connectionEntity = await _context.UserConnections
                .Include(connection => connection.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    connection => connection.Type == request.ConnectionType
                                  && connection.ExternalId == request.ConnectionId
                                  && connection.User.ArchivedAt == null,
                    cancellationToken);

            if (connectionEntity == default)
                throw new GoNotFoundException(nameof(User));

            return new UserResponse
            {
                Id = connectionEntity.UserId,
                FirstName = connectionEntity.User.FirstName,
                LastName = connectionEntity.User.LastName,
                AllowedScopes = ScopeHelper.GetScopes(connectionEntity.User.Roles),
                Nickname = connectionEntity.Nickname,
                ConnectionId = connectionEntity.ExternalId,
                Scope = connectionEntity.CurrentScope
            };
        }
    }
}
