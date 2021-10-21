using GO.Service.Movies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies.Queries.GetMovieList
{
    internal sealed class GetMovieListHandler
        : IRequestHandler<GetMovieListQuery, List<MovieResponse>>
    {
        private readonly MovieDbContext _context;

        public GetMovieListHandler(MovieDbContext context)
        {
            _context = context;
        }

        public Task<List<MovieResponse>> Handle(
            GetMovieListQuery request,
            CancellationToken cancellationToken)
        {
            return _context.UserMovies
                .AsNoTracking()
                .Include(movie => movie.Movie)
                .Where(movie => movie.IsWatchLater == request.IsWatchLater)
                .OrderByDescending(movie => movie.LastWatchedAt)
                .ThenByDescending(movie => movie.CreatedAt)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(movie => new MovieResponse
                {
                    Id = movie.MovieId,
                    ExternalId = movie.Movie.ExternalId,
                    Source = movie.Movie.Source,
                    Title = movie.Movie.Title,
                    ImageUrl = movie.Movie.ImageUrl,
                    Type = movie.Movie.Type,
                    Description = movie.Movie.Description,
                    Notes = movie.Notes,
                    CreatedAt = movie.CreatedAt,
                    LastWatchedAt = movie.LastWatchedAt,
                    IsWatchLater = movie.IsWatchLater
                })
                .ToListAsync(cancellationToken);
        }
    }
}
