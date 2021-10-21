using GO.HostBuilder.Exceptions;
using GO.Service.Users.Entities;
using GO.Service.Users.Enums;
using GO.Service.Users.Helpers;
using GO.Service.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users.Queries.GetUser
{
    internal sealed class GetUserHandler
        : IRequestHandler<GetUserQuery, UserResponse>
    {
        private readonly UserDbContext _context;

        public GetUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await _context.Users
                .Include(user => user.Connections
                    .Where(connection => request.ConnectionType == ConnectionType.Unsupported
                                         || connection.Type == request.ConnectionType))
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    user => user.ArchivedAt == null
                            && user.Id == request.UserId, cancellationToken);

            if (userEntity == default
                || request.ConnectionType != default
                && userEntity.Connections.All(connection => connection.ExternalId == string.Empty))
                throw new GoNotFoundException(nameof(User));

            var targetConnection = userEntity.Connections.FirstOrDefault();

            return new UserResponse
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                AllowedScopes = ScopeHelper.GetScopes(userEntity.Roles),
                Nickname = targetConnection?.Nickname,
                ConnectionId = targetConnection?.ExternalId,
                Scope = targetConnection?.CurrentScope ?? default
            };
        }
    }
}
