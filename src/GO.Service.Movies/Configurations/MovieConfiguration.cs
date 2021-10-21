using GO.HostBuilder.Constants;
using GO.Service.Movies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GO.Service.Movies.Configurations
{
    internal sealed class MovieConfiguration
        : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(movie => new { movie.Id });

            builder.HasIndex(movie => new { movie.ExternalId, movie.Source })
                .IsUnique();

            builder.Property(movie => movie.Source)
                .IsRequired();

            builder.Property(movie => movie.ExternalId)
                .HasMaxLength(ValidationConstants.StringMaxLength)
                .IsRequired();

            builder.Property(movie => movie.Title)
                .HasMaxLength(ValidationConstants.StringMaxLength)
                .IsRequired();

            builder.Property(movie => movie.Description)
                .HasMaxLength(ValidationConstants.StringMaxLength);

            builder.Property(movie => movie.ImageUrl)
                .HasMaxLength(ValidationConstants.StringMaxLength);

            builder.Property(movie => movie.Type)
                .HasMaxLength(ValidationConstants.StringMaxLength);
        }
    }
}
