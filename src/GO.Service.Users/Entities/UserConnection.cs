using GO.Service.Users.Enums;

namespace GO.Service.Users.Entities
{
    public sealed class UserConnection
    {
        public Guid UserId { get; set; }

        public ConnectionType Type { get; set; }

        public string Nickname { get; set; } = default!;

        public string ExternalId { get; set; } = default!;

        public Scopes CurrentScope { get; set; }

        public User User { get; set; } = default!;
    }
}
