using MediatR;

namespace GO.Service.Users.Commands.RegisterTelegramUser
{
    public sealed record RegisterTelegramUserCommand
        : IRequest<Unit>
    {
        public Guid UserId { get; init; }

        public string FirstName { get; init; } = default!;

        public string LastName { get; init; } = default!;

        public string Nickname { get; init; } = default!;

        public long TelegramId { get; init; }

        public RegisterTelegramUserCommand(
            Guid userId,
            string? firstName,
            string? lastName,
            string? nickname,
            long? telegramId)
        {
            UserId = userId;
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Nickname = nickname ?? string.Empty;
            TelegramId = telegramId ?? default;
        }
    }
}
