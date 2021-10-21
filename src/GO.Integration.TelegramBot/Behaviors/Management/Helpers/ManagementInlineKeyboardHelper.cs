using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace GO.Integration.TelegramBot.Behaviors.Management.Helpers
{
    internal static class ManagementInlineKeyboardHelper
    {
        public static IReplyMarkup GetLockUserKeyboard(Guid userId, ActionType type) =>
            GetKeyboard(new List<Dictionary<string, string>>
            {
                new ()
                {
                    {GetActionIcon(type), $"/{CommandType.Management} {type} {userId.ToAlphanumeric()}"},
                }
            });

        public static string GetActionIcon(ActionType action) =>
            action switch
            {
                ActionType.None => "🔧",
                ActionType.Decline => "❌",
                ActionType.Approve => "✔",
                ActionType.Change => "⚙",
                _ => throw new ArgumentOutOfRangeException(nameof(ActionType), action, null)
            };

        private static InlineKeyboardMarkup GetKeyboard(IEnumerable<IDictionary<string, string>> buttons) =>
            new(buttons
                .Select(row => row
                    .Select(cell => InlineKeyboardButton.WithCallbackData(
                        cell.Key,
                        cell.Value.ToLower()))
                )
            );
    }
}
