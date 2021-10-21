using GO.Service.Movies.Enums;

namespace GO.Service.Movies.Entities
{
    public sealed class Movie
    {
        public Guid Id { get; set; }

        public string ExternalId { get; set; } = default!;

        public MovieSource Source { get; set; }

        public string Title { get; set; } = default!;

        public string? Type { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
