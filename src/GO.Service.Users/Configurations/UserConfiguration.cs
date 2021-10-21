using GO.Service.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GO.Service.Users.Configurations
{
    internal sealed class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(user => user.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
