using GO.Integration.TelegramBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace GO.Integration.TelegramBot.Services
{
    internal class TelegramBotClientService
        : ITelegramBotClientService
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public TelegramBotClientService(
            ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public Task<Message> SendTextAsync(
            long chatId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.SendTextMessageAsync(
                chatId,
                message,
                replyMarkup: markup,
                cancellationToken: cancellationToken);

        public Task<Message> SendPhotoAsync(
            long chatId,
            string message,
            Uri photoUrl,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.SendPhotoAsync(
                new ChatId(chatId),
                new InputOnlineFile(photoUrl),
                message,
                replyMarkup: markup,
                cancellationToken: cancellationToken);

        public Task<Message> SendStickerAsync(
            long chatId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.SendStickerAsync(
                new ChatId(chatId),
                new InputOnlineFile(fileId),
                cancellationToken: cancellationToken);

        public Task<Message> UpdateTextAsync(
            long chatId,
            int messageId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.EditMessageTextAsync(
                new ChatId(chatId),
                messageId,
                message,
                replyMarkup: markup as InlineKeyboardMarkup,
                cancellationToken: cancellationToken);

        public Task<Message> UpdateCaptionAsync(
            long chatId,
            int messageId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.EditMessageCaptionAsync(
                new ChatId(chatId),
                messageId,
                message,
                replyMarkup: markup as InlineKeyboardMarkup,
                cancellationToken: cancellationToken);

        public Task DeleteAsync(
            long chatId,
            int messageId,
            CancellationToken cancellationToken = default) =>
            _telegramBotClient.DeleteMessageAsync(new ChatId(chatId), messageId, cancellationToken);
    }
}
