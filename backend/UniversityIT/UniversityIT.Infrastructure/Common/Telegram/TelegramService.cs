using Microsoft.Extensions.Options;
using Telegram.Bot;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Application.ValueObjects;

namespace UniversityIT.Infrastructure.Common.Telegram
{
    public class TelegramService : IMessageService
    {
        private readonly ITelegramBotClient _botClient;

        private readonly TelegramOptions _telegramOptions;

        public TelegramService(IOptions<TelegramOptions> options)
        {
            _telegramOptions = options.Value;

            var token = _telegramOptions.TelegramBotToken;
            _botClient = new TelegramBotClient(token);
        }

        public ITelegramBotClient GetBotClient()
        {
            return _botClient;
        }

        public async Task<bool> SendMessage(MessageReceiver receiver, string subject, string message)
        {
            bool success = false;
            
            long address = receiver.LongAddress;
            if (address == 0)
            {
                address = _telegramOptions.AdminChatId;
            }

            try
            {
                await _botClient.SendTextMessageAsync(address, message);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex);
            }
            
            return success;
        }
    }
}