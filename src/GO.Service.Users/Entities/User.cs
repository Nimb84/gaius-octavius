using GO.Service.Users.Enums;

namespace GO.Service.Users.Entities
{
    public sealed class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public Roles Roles { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LockedEnd { get; set; }

        public Guid? LockedBy { get; set; }

        public DateTimeOffset? ArchivedAt { get; set; }

        public Guid? ArchivedBy { get; set; }

        public ICollection<UserConnection> Connections { get; set; } = new List<UserConnection>();
    }
}
