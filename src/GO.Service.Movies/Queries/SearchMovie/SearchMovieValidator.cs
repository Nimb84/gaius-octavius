using FluentValidation;
using GO.HostBuilder.Constants;

namespace GO.Service.Movies.Queries.SearchMovie
{
    internal sealed class SearchMovieValidator
        : AbstractValidator<SearchMovieQuery>
    {
        public SearchMovieValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.Query)
                .NotEmpty()
                .MaximumLength(ValidationConstants.StringMaxLength);
        }
    }
}
