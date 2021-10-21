using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Service.Movies.Commands.DeleteWatchItem
{
    internal sealed class DeleteWatchItemHandler
        : IRequestHandler<DeleteWatchItemCommand>
    {
        private readonly MovieDbContext _context;

        public DeleteWatchItemHandler(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteWatchItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.UserMovies
                .FirstOrDefaultAsync(
                    movie => movie.MovieId == request.MovieId
                             && movie.UserId == request.UserId,
                    cancellationToken);

            if (entity == default)
                return Unit.Value;

            _context.UserMovies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
