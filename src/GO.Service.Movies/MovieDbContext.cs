using GO.Service.Movies.Entities;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies
{
    public sealed class MovieDbContext
        : DbContext
    {
        public DbSet<UserMovie> UserMovies { get; set; } = default!;
        public DbSet<Movie> Movies { get; set; } = default!;

        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserMovie>().ToTable(nameof(UserMovie));

            builder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
