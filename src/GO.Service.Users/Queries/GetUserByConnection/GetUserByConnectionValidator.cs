using FluentValidation;

namespace GO.Service.Users.Queries.GetUserByConnection
{
    internal sealed class GetUserByConnectionValidator
        : AbstractValidator<GetUserByConnectionQuery>
    {
        public GetUserByConnectionValidator()
        {
            RuleFor(item => item.ConnectionId)
                .NotEmpty();

            RuleFor(item => item.ConnectionType)
                .IsInEnum()
                .NotEmpty();
        }
    }
}
