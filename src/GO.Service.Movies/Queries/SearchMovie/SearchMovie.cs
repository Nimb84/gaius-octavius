using GO.Integration.IMDb.Abstractions;
using GO.Service.Movies.Entities;
using GO.Service.Movies.Enums;
using GO.Service.Movies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies.Queries.SearchMovie
{
    internal sealed class SearchMovie
        : IRequestHandler<SearchMovieQuery, List<MovieResponse>>
    {
        private readonly IMovieService _movieService;
        private readonly MovieDbContext _context;

        public SearchMovie(
            IMovieService movieService,
            MovieDbContext context)
        {
            _movieService = movieService;
            _context = context;
        }

        public async Task<List<MovieResponse>> Handle(
            SearchMovieQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _movieService.Search(request.Query);

            if (!result.Any())
            {
                return new List<MovieResponse>();
            }

            var resultIdList = result.Select(item => item.ExternalId).ToList();
            var movieList = await _context.Movies
                .Include(movie => movie.UserMovies
                    .Where(user => user.UserId == request.UserId))
                .Where(movie => resultIdList.Contains(movie.ExternalId))
                .ToListAsync(cancellationToken);

            if (movieList.Count != result.Count)
            {
                var entitiesToAdd = result
                    .Where(item => resultIdList
                        .Except(movieList.Select(movie => movie.ExternalId))
                        .Contains(item.ExternalId))
                    .Select(item => new Movie
                    {
                        Id = Guid.NewGuid(),
                        ExternalId = item.ExternalId,
                        Source = MovieSource.IMDb,
                        Title = item.Title,
                        Type = item.ResultType,
                        Description = item.Description,
                        ImageUrl = item.Image
                    })
                    .ToList();

                await _context.Movies.AddRangeAsync(entitiesToAdd, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                movieList.AddRange(entitiesToAdd);
            }

            return movieList
                .Select(movie => new MovieResponse(movie))
                .ToList();
        }
    }
}
