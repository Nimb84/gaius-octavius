using Telegram.Bot.Types;

namespace GO.Integration.TelegramBot.Behaviors.Factory
{
    internal interface IBotBehaviorFactory
    {
        Task HandleUpdateAsync(Update model, CancellationToken cancellationToken = default);
    }
}
