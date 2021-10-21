using GO.Service.Movies.Models;
using MediatR;

namespace GO.Service.Movies.Queries.GetMovieList
{
    public sealed record GetMovieListQuery
        : IRequest<List<MovieResponse>>
    {
        public Guid UserId { get; init; }

        public bool IsWatchLater { get; init; }

        public int Skip { get; init; } = 0;

        public int Take { get; init; } = 10;
    }
}
