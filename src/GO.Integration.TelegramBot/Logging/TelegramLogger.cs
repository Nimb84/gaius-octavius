using System.Text;
using GO.Integration.TelegramBot.Abstractions;
using GO.Integration.TelegramBot.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GO.Integration.TelegramBot.Logging
{
    internal sealed class TelegramBotLogger
        : ILogger
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _loggerName;
        private readonly Func<TelegramBotLoggerConfiguration> _getCurrentConfig;

        private ITelegramBotClientService? _botClient;

        private ITelegramBotClientService TelegramBotClient =>
            _botClient ??= _serviceProvider.GetRequiredService<ITelegramBotClientService>();

        public TelegramBotLogger(
            string name,
            Func<TelegramBotLoggerConfiguration> getCurrentConfig,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            (_loggerName, _getCurrentConfig) = (name, getCurrentConfig);
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull =>
            default!;

        public bool IsEnabled(LogLevel logLevel) =>
            logLevel >= _getCurrentConfig().Level;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var hasException = exception != null;
            var builder = new StringBuilder(
                $"[{eventId.Id}: {logLevel}{(hasException ? "❗️" : string.Empty)}]");

            builder
                .AppendLine()
                .Append(state);

            if (hasException)
            {
                builder
                    .AppendLine()
                    .AppendLine()
                    .Append(exception?.ToString()[.._getCurrentConfig().Length]);
            }

            TelegramBotClient
                .SendTextAsync(
                    _getCurrentConfig().ChatId,
                    builder.ToString())
                .Wait();
        }
    }
}
