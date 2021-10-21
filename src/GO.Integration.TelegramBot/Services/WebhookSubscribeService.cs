using GO.HostBuilder.Configurations;
using GO.Integration.TelegramBot.Configurations;
using GO.Integration.TelegramBot.Constants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace GO.Integration.TelegramBot.Services
{
    internal class WebhookSubscribeService
        : IHostedService
    {
        private readonly ILogger<WebhookSubscribeService> _logger;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly AppConfiguration _appConfiguration;
        private readonly TelegramBotConfiguration _botConfiguration;

        public WebhookSubscribeService(
            ILogger<WebhookSubscribeService> logger,
            ITelegramBotClient telegramBotClient,
            IOptions<TelegramBotConfiguration> botConfiguration,
            IOptions<AppConfiguration> appConfiguration)
        {
            _logger = logger;
            _telegramBotClient = telegramBotClient;
            _appConfiguration = appConfiguration.Value;
            _botConfiguration = botConfiguration.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _telegramBotClient.SetWebhookAsync(
                $"{_appConfiguration.Domain}{_botConfiguration.WebhookUrl}",
                allowedUpdates: TelegramConstants.AllowedUpdateTypes,
                cancellationToken: cancellationToken);

            _logger.LogInformation("Application started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _telegramBotClient.DeleteWebhookAsync(cancellationToken: cancellationToken);

            _logger.LogInformation("Application stopped");
        }
    }
}
