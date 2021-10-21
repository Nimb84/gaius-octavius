using GO.Service.Movies.Models;
using MediatR;

namespace GO.Service.Movies.Queries.SearchMovie
{
    public sealed record SearchMovieQuery(Guid UserId, string Query)
        : IRequest<List<MovieResponse>>;
}
