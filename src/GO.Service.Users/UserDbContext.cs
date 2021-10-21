using GO.Service.Users.Entities;
using GO.Service.Users.Seeds;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Users
{
    public sealed class UserDbContext
        : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserConnection> UserConnections { get; set; } = default!;

        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<UserConnection>().ToTable(nameof(UserConnection));

            builder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);

            builder.AddUserSeedData();

            base.OnModelCreating(builder);
        }
    }
}
