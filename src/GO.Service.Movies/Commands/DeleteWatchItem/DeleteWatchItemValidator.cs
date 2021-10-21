using FluentValidation;

namespace GO.Service.Movies.Commands.DeleteWatchItem
{
    internal sealed class DeleteWatchItemValidator
        : AbstractValidator<DeleteWatchItemCommand>
    {
        public DeleteWatchItemValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.MovieId)
                .NotEmpty();
        }
    }
}
