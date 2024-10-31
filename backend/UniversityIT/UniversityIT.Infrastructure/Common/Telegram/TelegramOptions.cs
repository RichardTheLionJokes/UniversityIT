namespace UniversityIT.Infrastructure.Common.Telegram
{
    public class TelegramOptions
    {
        public string TelegramBotToken { get; set; } = string.Empty;
        public long AdminChatId { get; set; }
    }
}