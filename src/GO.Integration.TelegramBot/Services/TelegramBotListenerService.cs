using GO.HostBuilder.Exceptions;
using GO.Integration.TelegramBot.Abstractions;
using GO.Integration.TelegramBot.Behaviors.Factory;
using GO.Integration.TelegramBot.Extensions;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace GO.Integration.TelegramBot.Services
{
    internal class TelegramBotListenerService
        : ITelegramBotListenerService
    {
        private readonly ILogger<TelegramBotListenerService> _logger;
        private readonly IBotBehaviorFactory _botBehaviorFactory;
        private readonly ITelegramBotClientService _telegramBotClientService;

        public TelegramBotListenerService(
            ILogger<TelegramBotListenerService> logger,
            IBotBehaviorFactory botBehaviorFactory,
            ITelegramBotClientService telegramBotClientService)
        {
            _logger = logger;
            _botBehaviorFactory = botBehaviorFactory;
            _telegramBotClientService = telegramBotClientService;
        }

        public async Task EchoUpdateAsync(Update model, CancellationToken cancellationToken)
        {
            try
            {
                await _botBehaviorFactory.HandleUpdateAsync(model, cancellationToken);
            }
            catch (GoException ex)
            {
                _logger.LogError(ex, ex.Title);

                await _telegramBotClientService.SendTextAsync(
                    model.GetChatId(),
                    ex.Title,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
