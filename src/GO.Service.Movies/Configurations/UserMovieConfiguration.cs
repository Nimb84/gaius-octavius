using GO.HostBuilder.Constants;
using GO.Service.Movies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GO.Service.Movies.Configurations
{
    internal sealed class UserMovieConfiguration
        : IEntityTypeConfiguration<UserMovie>
    {
        public void Configure(EntityTypeBuilder<UserMovie> builder)
        {
            builder.HasKey(userMovie => new { userMovie.UserId, userMovie.MovieId });

            builder.Property(userMovie => userMovie.Notes)
                .HasMaxLength(ValidationConstants.StringMaxLength);

            builder.HasOne(userMovie => userMovie.Movie)
                .WithMany(movie => movie.UserMovies)
                .HasForeignKey(userMovie => userMovie.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
