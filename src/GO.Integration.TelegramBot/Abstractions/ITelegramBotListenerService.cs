using Telegram.Bot.Types;

namespace GO.Integration.TelegramBot.Abstractions
{
    public interface ITelegramBotListenerService
    {
        Task EchoUpdateAsync(Update model, CancellationToken cancellationToken);
    }
}
