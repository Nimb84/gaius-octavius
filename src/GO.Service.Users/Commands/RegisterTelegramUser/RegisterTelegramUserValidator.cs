using FluentValidation;

namespace GO.Service.Users.Commands.RegisterTelegramUser
{
    internal sealed class RegisterTelegramUserValidator
        : AbstractValidator<RegisterTelegramUserCommand>
    {
        public RegisterTelegramUserValidator()
        {
            RuleFor(item => item.UserId)
                .NotEmpty();

            RuleFor(item => item.FirstName)
                .NotEmpty();

            RuleFor(item => item.LastName)
                .NotEmpty();

            RuleFor(item => item.Nickname)
                .NotEmpty();

            RuleFor(item => item.TelegramId)
                .NotEmpty();
        }
    }
}
