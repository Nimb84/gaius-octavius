namespace GO.Integration.TelegramBot.Configurations
{
    public sealed class TelegramBotConfiguration
    {
        public string Token { get; set; } = default!;

        public long AdminChatId { get; set; }

        public string WebhookUrl { get; set; } = default!;
    }
}
