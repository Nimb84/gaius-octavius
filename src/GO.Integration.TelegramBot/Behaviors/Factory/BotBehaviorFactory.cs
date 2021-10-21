using GO.Integration.TelegramBot.Behaviors.Management;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using GO.Integration.TelegramBot.Behaviors.Movie;
using GO.Integration.TelegramBot.Extensions;
using GO.Service.Users.Enums;
using GO.Service.Users.Models;
using GO.Service.Users.Queries.GetUserByConnection;
using MediatR;
using Telegram.Bot.Types;

namespace GO.Integration.TelegramBot.Behaviors.Factory
{
    internal class BotBehaviorFactory
        : IBotBehaviorFactory
    {
        private readonly IMediator _mediator;
        private readonly IManagementBotBehavior _managementBotBehavior;
        private readonly IMovieBotBehavior _movieBotBehavior;

        public BotBehaviorFactory(
            IMediator mediator,
            IManagementBotBehavior managementBotBehavior,
            IMovieBotBehavior movieBotBehavior)
        {
            _mediator = mediator;
            _managementBotBehavior = managementBotBehavior;
            _movieBotBehavior = movieBotBehavior;
        }

        public Task HandleUpdateAsync(Update model, CancellationToken cancellationToken = default) =>
            model.IsCommand(out var command)
                ? HandleAsCommandAsync(command, model, cancellationToken)
                : HandleAsMessageAsync(model, cancellationToken);

        private async Task HandleAsCommandAsync(
            CommandType command,
            Update model,
            CancellationToken cancellationToken)
        {
            var user = command != CommandType.Start
                ? await GetUserAsync(model.GetTelegramId(), cancellationToken)
                : null;

            IBaseCommandChatBotBehavior behavior = command switch
            {
                CommandType.Start => _managementBotBehavior,
                CommandType.Information => _managementBotBehavior,
                CommandType.Management => _managementBotBehavior,
                CommandType.Movie => _movieBotBehavior,
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            };

            await behavior.HandleCommandAsync(user, model, cancellationToken);
        }

        private async Task HandleAsMessageAsync(
            Update model,
            CancellationToken cancellationToken)
        {
            var user = await GetUserAsync(model.GetTelegramId(), cancellationToken);

            IBaseMessageChatBotBehavior behavior = user.Scope switch
            {
                Scopes.Movies => _movieBotBehavior,
                _ => throw new ArgumentOutOfRangeException(nameof(user.Scope), user.Scope, null)
            };

            await behavior.HandleMessageAsync(user, model, cancellationToken);
        }

        private Task<UserResponse> GetUserAsync(
            long telegramId,
            CancellationToken cancellationToken) =>
            _mediator.Send(new GetUserByConnectionQuery(telegramId.ToString(), ConnectionType.Telegram), cancellationToken);
    }
}
