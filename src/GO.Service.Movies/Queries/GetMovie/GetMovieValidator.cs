using FluentValidation;

namespace GO.Service.Movies.Queries.GetMovie
{
    internal sealed class GetMovieValidator
        : AbstractValidator<GetMovieQuery>
    {
        public GetMovieValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.MovieId)
                .NotEmpty();
        }
    }
}
