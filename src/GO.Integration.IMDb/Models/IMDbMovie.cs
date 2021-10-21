namespace GO.Integration.IMDb.Models
{
    public record IMDbMovie
    {
        public string ExternalId { get; init; } = default!;

        public string ResultType { get; init; } = default!;

        public string Image { get; init; } = default!;

        public string Title { get; init; } = default!;

        public string Description { get; init; } = default!;
    }
}
