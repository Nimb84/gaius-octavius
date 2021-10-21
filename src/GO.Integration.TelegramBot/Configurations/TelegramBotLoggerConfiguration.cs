using Microsoft.Extensions.Logging;

namespace GO.Integration.TelegramBot.Configurations
{
    public sealed class TelegramBotLoggerConfiguration
    {
        public long ChatId { get; set; }

        public LogLevel Level { get; set; }

        public ushort Length { get; set; } = 1000;
    }
}
