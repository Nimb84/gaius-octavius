using FluentValidation;

namespace GO.Service.Users.Commands.LockUser
{
    internal sealed class LockUserValidator
        : AbstractValidator<LockUserCommand>
    {
        public LockUserValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.CurrentUserId)
                .NotEmpty()
                .NotEqual(item => item.UserId);
        }
    }
}
