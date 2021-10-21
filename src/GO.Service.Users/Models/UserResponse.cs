using GO.Service.Users.Enums;

namespace GO.Service.Users.Models
{
    public sealed record UserResponse
    {
        public Guid Id { get; init; }

        public string FirstName { get; init; } = default!;

        public string LastName { get; init; } = default!;

        public string? Nickname { get; init; }

        public string? ConnectionId { get; init; }

        public Scopes AllowedScopes { get; init; }

        public Scopes Scope { get; init; }
    }
}
