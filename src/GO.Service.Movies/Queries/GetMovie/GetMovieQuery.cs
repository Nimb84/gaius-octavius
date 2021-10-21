using GO.Service.Movies.Models;
using MediatR;

namespace GO.Service.Movies.Queries.GetMovie
{
    public sealed record GetMovieQuery(Guid UserId, Guid MovieId)
        : IRequest<MovieResponse>;
}
