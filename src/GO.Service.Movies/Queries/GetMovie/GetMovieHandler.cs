using GO.HostBuilder.Exceptions;
using GO.Service.Movies.Entities;
using GO.Service.Movies.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies.Queries.GetMovie
{
    internal sealed class GetMovieHandler
        : IRequestHandler<GetMovieQuery, MovieResponse>
    {
        private readonly MovieDbContext _context;

        public GetMovieHandler(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<MovieResponse> Handle(
            GetMovieQuery request,
            CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .Include(movie => movie.UserMovies
                    .Where(user => user.UserId == request.UserId))
                .FirstOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);

            if (movie == default)
                throw new GoNotFoundException(nameof(Movie));

            return new MovieResponse(movie);
        }
    }
}
