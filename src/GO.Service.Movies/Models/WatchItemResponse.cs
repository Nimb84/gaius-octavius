using GO.Service.Movies.Entities;
using GO.Service.Movies.Enums;

namespace GO.Service.Movies.Models
{
    public sealed record MovieResponse
    {
        public Guid Id { get; init; }

        public string ExternalId { get; init; } = default!;

        public MovieSource Source { get; init; }

        public string Title { get; init; } = default!;

        public string? ImageUrl { get; init; }

        public string? Type { get; init; }

        public string? Description { get; init; }

        public string? Notes { get; init; }

        public DateTimeOffset? CreatedAt { get; init; }

        public DateTimeOffset? LastWatchedAt { get; init; }

        public bool IsWatchLater { get; init; }

        public MovieResponse()
        {

        }

        public MovieResponse(Movie entity)
        {
            Id = entity.Id;
            ExternalId = entity.ExternalId;
            Source = entity.Source;
            Title = entity.Title;
            ImageUrl = entity.ImageUrl;
            Type = entity.Type;
            Description = entity.Description;
            Notes = entity.UserMovies.FirstOrDefault()?.Notes;
            CreatedAt = entity.UserMovies.FirstOrDefault()?.CreatedAt;
            LastWatchedAt = entity.UserMovies.FirstOrDefault()?.LastWatchedAt;
            IsWatchLater = entity.UserMovies.FirstOrDefault()?.IsWatchLater ?? false;
        }
    }
}
