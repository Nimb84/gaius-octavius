using Telegram.Bot.Types.Enums;

namespace GO.Integration.TelegramBot.Constants
{
    internal static class TelegramConstants
    {
        public const string HttpClientName = "TelegramWebhookClient";

        public static List<UpdateType> AllowedUpdateTypes =>
            new() { UpdateType.Message, UpdateType.CallbackQuery };
    }
}
