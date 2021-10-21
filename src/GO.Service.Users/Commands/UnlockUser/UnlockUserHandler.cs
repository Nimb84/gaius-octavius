using GO.HostBuilder.Exceptions;
using GO.Service.Users.Entities;
using GO.Service.Users.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users.Commands.UnlockUser
{
    internal sealed class UnlockUserHandler
        : IRequestHandler<UnlockUserCommand>
    {
        private readonly UserDbContext _context;

        public UnlockUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UnlockUserCommand request,
            CancellationToken cancellationToken)
        {
            var entities = await _context.Users
                .Where(user => user.Id == request.UserId || user.Id == request.CurrentUserId)
                .ToListAsync(cancellationToken);

            var currentUser = entities.FirstOrDefault(user => user.Id == request.CurrentUserId);
            if (currentUser == default || !currentUser.Roles.HasFlag(Roles.Administration))
                throw new GoForbiddenException();

            var targetUser = entities.FirstOrDefault(user => user.Id == request.UserId);
            if (targetUser == default)
                throw new GoNotFoundException(nameof(User));

            targetUser.LockedEnd = null;
            targetUser.LockedBy = currentUser.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
