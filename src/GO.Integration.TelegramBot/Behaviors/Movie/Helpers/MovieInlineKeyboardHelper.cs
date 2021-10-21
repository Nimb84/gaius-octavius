using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using GO.Integration.TelegramBot.Behaviors.Movie.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace GO.Integration.TelegramBot.Behaviors.Movie.Helpers
{
    internal static class MovieInlineKeyboardHelper
    {
        public static IReplyMarkup GetKeyboard(
            Guid movieId,
            List<MovieActionType> actions)
        {
            var actionDictionary = actions.ToDictionary(
                GetActionIcon,
                action => $"/{CommandType.Movie} {action} {movieId.ToAlphanumeric()}");

            return GetKeyboard(new List<Dictionary<string, string>>
            {
                actionDictionary
            });
        }

        private static string GetActionIcon(MovieActionType action) =>
            action switch
            {
                MovieActionType.MarkAsWatched => "✔",
                MovieActionType.WatchLater => "👀",
                MovieActionType.Delete => "❌",
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
