using FluentValidation;

namespace GO.Service.Users.Queries.GetUser
{
    internal sealed class GetUserValidator
        : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.ConnectionType)
                .IsInEnum()
                .NotEmpty();
        }
    }
}
