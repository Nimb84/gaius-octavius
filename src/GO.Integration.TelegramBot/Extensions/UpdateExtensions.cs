using GO.HostBuilder.Enums;
using GO.HostBuilder.Exceptions;
using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using GO.Integration.TelegramBot.Models;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GO.Integration.TelegramBot.Extensions
{
    internal static class UpdateExtensions
    {
        public static long GetTelegramId(this Update model)
        {
            var telegramId = model.Type switch
            {
                UpdateType.Message => model.Message?.From?.Id,
                UpdateType.EditedMessage => model.EditedMessage?.From?.Id,
                UpdateType.CallbackQuery => model.CallbackQuery?.From.Id,
                _ => throw new ArgumentOutOfRangeException(nameof(UpdateType), model.Type, null)
            };

            if (!telegramId.HasValue)
                throw new ArgumentNullException(nameof(telegramId));

            return telegramId.Value;
        }

        public static string? GetText(this Update model) =>
            model.Type switch
            {
                UpdateType.Message => model.Message?.Text,
                UpdateType.CallbackQuery => model.CallbackQuery?.Message?.Text,
                _ => throw new ArgumentOutOfRangeException(nameof(model.Type), model.Type, null)
            };

        public static string GetCommand(this Update model) =>
            model.Type switch
            {
                UpdateType.Message => model.Message?.Text ?? string.Empty,
                UpdateType.CallbackQuery => model.CallbackQuery?.Data ?? string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(model.Type), model.Type, null)
            };

        public static bool IsCommand(this Update model) =>
            model.IsCommand(out _);

        public static bool IsCommand(this Update model, out CommandType type)
        {
            type = model.Type is UpdateType.Message or UpdateType.CallbackQuery
                ? EnumExtensions.Parse<CommandType>(model.GetCommand().Split().First()[1..])
                : CommandType.None;

            return type != CommandType.None;
        }

        public static CommandRequest ToCommandRequest(this Update model) =>
            model.IsCommand()
                ? new CommandRequest(model.GetCommand())
                : throw new GoException(StatusCodes.Status400BadRequest, ExceptionType.Unsupported);

        public static bool IsBot(this Update model) =>
            model.Type switch
            {
                UpdateType.Message => model.Message?.From?.IsBot ?? true,
                UpdateType.EditedMessage => model.EditedMessage?.From?.IsBot ?? true,
                UpdateType.CallbackQuery => model.CallbackQuery?.From?.IsBot ?? true,
                _ => throw new ArgumentOutOfRangeException(nameof(UpdateType), model.Type, null)
            };

        public static long GetChatId(this Update model)
        {
            var chatId = model.Type switch
            {
                UpdateType.Message => model.Message?.Chat.Id,
                UpdateType.EditedMessage => model.EditedMessage?.Chat.Id,
                UpdateType.CallbackQuery => model.CallbackQuery?.Message?.Chat.Id,
                _ => throw new ArgumentOutOfRangeException(nameof(UpdateType), model.Type, null)
            };

            if (!chatId.HasValue)
                throw new ArgumentNullException(nameof(chatId));

            return chatId.Value;
        }

        public static int GetMessageId(this Update model)
        {
            var messageId = model.Type switch
            {
                UpdateType.Message => model.Message?.MessageId,
                UpdateType.EditedMessage => model.EditedMessage?.MessageId,
                UpdateType.CallbackQuery => model.CallbackQuery?.Message?.MessageId,
                _ => throw new ArgumentOutOfRangeException(nameof(UpdateType), model.Type, null)
            };

            if (!messageId.HasValue)
                throw new ArgumentNullException(nameof(messageId));

            return messageId.Value;
        }
    }
}
