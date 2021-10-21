using GO.HostBuilder.Enums;
using GO.HostBuilder.Exceptions;
using GO.Service.Users.Entities;
using GO.Service.Users.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users.Commands.RegisterTelegramUser
{
    internal sealed class RegisterTelegramUserHandler
        : IRequestHandler<RegisterTelegramUserCommand>
    {
        private readonly UserDbContext _context;

        public RegisterTelegramUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            RegisterTelegramUserCommand request,
            CancellationToken cancellationToken)
        {
            var telegramId = request.TelegramId.ToString();
            var collision = await _context.UserConnections
                .AnyAsync(
                    connection => connection.Type == ConnectionType.Telegram
                                                && connection.ExternalId == telegramId,
                    cancellationToken);

            if (collision)
                throw new GoException(StatusCodes.Status409Conflict, ExceptionType.Conflict);

            var user = new User
            {
                Id = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Roles = Roles.User,
                CreatedAt = DateTimeOffset.UtcNow,
                Connections = new List<UserConnection>
                {
                    new()
                    {
                        UserId = request.UserId,
                        Type = ConnectionType.Telegram,
                        Nickname = request.Nickname,
                        ExternalId = telegramId,
                        CurrentScope = Scopes.Movies
                    }
                }
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
