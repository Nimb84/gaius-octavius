using FluentValidation;

namespace GO.Service.Movies.Commands.SaveAsWatched
{
    internal sealed class SaveAsWatchedValidator
        : AbstractValidator<SaveAsWatchedCommand>
    {
        public SaveAsWatchedValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.MovieId)
                .NotEmpty();
        }
    }
}
