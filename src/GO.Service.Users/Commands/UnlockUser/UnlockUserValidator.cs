using FluentValidation;

namespace GO.Service.Users.Commands.UnlockUser
{
    internal sealed class UnlockUserValidator
        : AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.CurrentUserId)
                .NotEmpty()
                .NotEqual(item => item.UserId);
        }
    }
}
