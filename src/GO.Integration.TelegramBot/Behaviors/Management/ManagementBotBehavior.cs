using System.Text;
using GO.HostBuilder.Exceptions;
using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Abstractions;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using GO.Integration.TelegramBot.Behaviors.Management.Helpers;
using GO.Integration.TelegramBot.Configurations;
using GO.Integration.TelegramBot.Extensions;
using GO.Integration.TelegramBot.Resources;
using GO.Service.Users.Commands.LockUser;
using GO.Service.Users.Commands.RegisterTelegramUser;
using GO.Service.Users.Commands.UnlockUser;
using GO.Service.Users.Enums;
using GO.Service.Users.Models;
using GO.Service.Users.Queries.GetUser;
using MediatR;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GO.Integration.TelegramBot.Behaviors.Management
{
    internal sealed class ManagementBotBehavior
        : IManagementBotBehavior
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotConfiguration _telegramBotConfiguration;
        private readonly ITelegramBotClientService _telegramBotClientService;

        public ManagementBotBehavior(
            IMediator mediator,
            IOptions<TelegramBotConfiguration> telegramBotConfiguration,
            ITelegramBotClientService telegramBotClientService)
        {
            _mediator = mediator;
            _telegramBotConfiguration = telegramBotConfiguration.Value;
            _telegramBotClientService = telegramBotClientService;
        }

        public Task HandleCommandAsync(
            UserResponse? currentUser,
            Update model,
            CancellationToken cancellationToken = default)
        {
            var command = model.ToCommandRequest().Type;

            if (command != CommandType.Start && currentUser == null)
                throw new GoForbiddenException();

            return command switch
            {
                CommandType.Start => RegisterUser(model, cancellationToken),
                CommandType.Management => ManagementUser(currentUser!, model, cancellationToken),
                CommandType.Information => SendUserInfo(currentUser!, model, cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(CommandType), command, null)
            };
        }

        private async Task RegisterUser(Update model, CancellationToken cancellationToken)
        {
            if (model.IsBot() || model.GetChatId() != model.GetTelegramId())
                throw new GoForbiddenException();

            var command = new RegisterTelegramUserCommand(
                Guid.NewGuid(),
                model.Message?.From?.FirstName,
                model.Message?.From?.LastName,
                model.Message?.From?.Username,
                model.Message?.From?.Id);

            await _mediator.Send(command, cancellationToken);

            await _telegramBotClientService.SendTextAsync(
                _telegramBotConfiguration.AdminChatId,
                string.Format(
                    MessageResources.NewUser_Format,
                    $"{command.FirstName} {command.LastName}", command.Nickname),
                ManagementInlineKeyboardHelper.GetLockUserKeyboard(command.UserId, ActionType.Decline),
                cancellationToken);
        }

        private Task ManagementUser(
            UserResponse currentUser,
            Update model,
            CancellationToken cancellationToken)
        {
            if (model.IsBot() || model.GetChatId() != _telegramBotConfiguration.AdminChatId)
                throw new GoForbiddenException();

            var command = model.ToCommandRequest();
            var action = EnumExtensions.Parse<ActionType>(command.Action);

            return action switch
            {
                ActionType.Decline => LockUser(
                    Guid.Parse(command.Arguments.First()),
                    currentUser.Id,
                    model,
                    cancellationToken),
                ActionType.Approve => UnlockUser(
                    Guid.Parse(command.Arguments.First()),
                    currentUser.Id,
                    model,
                    cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(ActionType), action, null)
            };
        }

        private async Task LockUser(
            Guid userId,
            Guid currentUserId,
            Update model,
            CancellationToken cancellationToken)
        {
            var command = new LockUserCommand(userId, currentUserId);

            await _mediator.Send(command, cancellationToken);

            if (model.Type == UpdateType.CallbackQuery)
            {
                await _telegramBotClientService.UpdateTextAsync(
                    model.GetChatId(),
                    model.GetMessageId(),
                    model.GetText() ?? string.Empty,
                    ManagementInlineKeyboardHelper.GetLockUserKeyboard(userId, ActionType.Approve),
                    cancellationToken);
            }
        }

        private async Task UnlockUser(
            Guid userId,
            Guid currentUserId,
            Update model,
            CancellationToken cancellationToken)
        {
            var command = new UnlockUserCommand(userId, currentUserId);

            await _mediator.Send(command, cancellationToken);

            if (model.Type == UpdateType.CallbackQuery)
            {
                await _telegramBotClientService.UpdateTextAsync(
                    model.GetChatId(),
                    model.GetMessageId(),
                    model.GetText() ?? string.Empty,
                    ManagementInlineKeyboardHelper.GetLockUserKeyboard(userId, ActionType.Decline),
                    cancellationToken);
            }
        }

        private async Task SendUserInfo(
            UserResponse currentUser,
            Update model,
            CancellationToken cancellationToken)
        {
            if (model.IsBot()
                || model.Message == null
                || model.Message.Chat.Id != model.Message.From?.Id)
                throw new GoForbiddenException();

            var query = new GetUserQuery(currentUser.Id, ConnectionType.Telegram);

            var user = await _mediator.Send(query, cancellationToken);

            await _telegramBotClientService.SendTextAsync(
                model.Message.Chat.Id,
                GetUserInfo(user),
                null,
                cancellationToken);
        }

        private static string GetUserInfo(UserResponse user)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Hi {user.FirstName} {user.LastName}");
            builder.AppendLine($"Your scopes: {user.AllowedScopes}");

            if (user.Scope != Scopes.None)
                builder.AppendLine($"{Environment.NewLine}Current service {user.Scope}");

            return builder.ToString();
        }
    }
}
