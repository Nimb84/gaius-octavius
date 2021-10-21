using FluentValidation;
using GO.HostBuilder.Constants;

namespace GO.Service.Movies.Queries.GetMovieList
{
    internal sealed class GetMovieListValidator
        : AbstractValidator<GetMovieListQuery>
    {
        public GetMovieListValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.Skip)
                .GreaterThan(0);

            RuleFor(item => item.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(ValidationConstants.MaxTake);
        }
    }
}
