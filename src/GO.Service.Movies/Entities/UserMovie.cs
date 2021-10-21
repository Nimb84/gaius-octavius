namespace GO.Service.Movies.Entities
{
    public sealed class UserMovie
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public string? Notes { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastWatchedAt { get; set; }

        public bool IsWatchLater { get; set; }

        public Movie Movie { get; set; } = default!;
    }
}
