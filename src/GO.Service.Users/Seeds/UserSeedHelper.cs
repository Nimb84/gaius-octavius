using GO.Service.Users.Entities;
using GO.Service.Users.Enums;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users.Seeds
{
    internal static class UserSeedHelper
    {
        public static ModelBuilder AddUserSeedData(this ModelBuilder builder)
        {
            var adminId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

            builder
                .Entity<User>()
                .HasData(new List<User>
                {
                    new()
                    {
                        Id = adminId,
                        FirstName = "Dmytro",
                        LastName = "😇",
                        Roles = Roles.Administration,
                        CreatedAt = DateTimeOffset.UtcNow
                    }
                });

            builder
                .Entity<UserConnection>()
                .HasData(new List<UserConnection>
                {
                    new()
                    {
                        UserId = adminId,
                        Type = ConnectionType.Telegram,
                        Nickname = "Nimb84",
                        ExternalId = "428296956",
                        CurrentScope = Scopes.Movies
                    }
                });

            return builder;
        }
    }
}
