using System.Collections.Concurrent;
using GO.Integration.TelegramBot.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GO.Integration.TelegramBot.Logging
{
    [ProviderAlias("TelegramBot")]
    internal sealed class TelegramBotLoggerProvider
        : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDisposable? _onChangeToken;
        private readonly ConcurrentDictionary<string, TelegramBotLogger> _loggers =
            new(StringComparer.OrdinalIgnoreCase);

        private TelegramBotLoggerConfiguration _loggerConfiguration;

        public TelegramBotLoggerProvider(
            IOptionsMonitor<TelegramBotLoggerConfiguration> loggerConfiguration,
            IServiceProvider serviceProvider)
        {
            if (loggerConfiguration.CurrentValue.ChatId == default)
            {
                throw new ArgumentNullException(nameof(TelegramBotLoggerConfiguration.ChatId));
            }

            _serviceProvider = serviceProvider;
            _loggerConfiguration = loggerConfiguration.CurrentValue;
            _onChangeToken = loggerConfiguration.OnChange(updatedConfig => _loggerConfiguration = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(
                categoryName,
                name => new TelegramBotLogger(name, GetCurrentConfig, _serviceProvider));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }

        private TelegramBotLoggerConfiguration GetCurrentConfig() =>
            _loggerConfiguration;
    }
}
