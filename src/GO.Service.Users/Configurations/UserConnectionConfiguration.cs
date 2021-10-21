using GO.HostBuilder.Constants;
using GO.Service.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GO.Service.Users.Configurations
{
    internal sealed class UserConnectionConfiguration
        : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.HasKey(connection => new { connection.UserId, connection.Type });

            builder.Property(connection => connection.Nickname)
                .HasMaxLength(ValidationConstants.StringMaxLength);

            builder.Property(connection => connection.ExternalId)
                .HasMaxLength(ValidationConstants.StringMaxLength)
                .IsRequired();
        }
    }
}
