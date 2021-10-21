using FluentValidation;

namespace GO.Service.Movies.Commands.SaveToWatchLater
{
    internal sealed class SaveToWatchLaterValidator
        : AbstractValidator<SaveToWatchLaterCommand>
    {
        public SaveToWatchLaterValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.MovieId)
                .NotEmpty();
        }
    }
}
