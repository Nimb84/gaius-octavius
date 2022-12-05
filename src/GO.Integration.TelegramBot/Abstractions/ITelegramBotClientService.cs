using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GO.Integration.TelegramBot.Abstractions
{
    internal interface ITelegramBotClientService
    {
        Task<Message> SendTextAsync(
            long chatId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default);

        Task<Message> SendPhotoAsync(
            long chatId,
            string message,
            Uri photoUrl,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default);

        Task<Message> SendStickerAsync(
            long chatId,
            string fileId,
            CancellationToken cancellationToken = default);

        Task<Message> UpdateTextAsync(
            long chatId,
            int messageId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default);

        Task<Message> UpdateCaptionAsync(
            long chatId,
            int messageId,
            string message,
            IReplyMarkup? markup = null,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long chatId,
            int messageId,
            CancellationToken cancellationToken = default);
    }
}
