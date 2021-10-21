using GO.HostBuilder.Exceptions;
using GO.Service.Movies.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies.Commands.SaveAsWatched
{
    internal sealed class SaveAsWatchedHandler
        : IRequestHandler<SaveAsWatchedCommand>
    {
        private readonly MovieDbContext _context;

        public SaveAsWatchedHandler(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            SaveAsWatchedCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.UserMovies
                .FirstOrDefaultAsync(
                    movie => movie.UserId == request.UserId
                             && movie.MovieId == request.MovieId,
                    cancellationToken);

            if (entity == default)
            {
                var movieExists = await _context.Movies
                    .AnyAsync(movie => movie.Id == request.MovieId, cancellationToken);

                if (!movieExists)
                    throw new GoNotFoundException(nameof(Movie));

                entity = new UserMovie
                {
                    UserId = request.UserId,
                    MovieId = request.MovieId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                await _context.UserMovies.AddAsync(entity, cancellationToken);
            }

            entity.IsWatchLater = false;
            entity.LastWatchedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
