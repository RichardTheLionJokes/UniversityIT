using Microsoft.Extensions.Hosting;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UniversityIT.Infrastructure.Common.Telegram;

namespace UniversityIT.Infrastructure.Background
{
    public class TelegramBotReceiver : BackgroundService
    {
        private readonly TelegramService _telegramService;

        public TelegramBotReceiver(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _botClient = _telegramService.GetBotClient();
            _botClient.StartReceiving(Update, Error);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1 * 10000, stoppingToken);
            }
        }

        private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            if (message != null)
                            {
                                var chat = message.Chat;
                                if (message.Text != null)
                                {
                                    if (message.Text.ToLower().StartsWith("/start"))
                                    {
                                        string userName = !String.IsNullOrEmpty(chat.Username) ? chat.Username : !String.IsNullOrEmpty(chat.Title) ? chat.Title : "";
                                        string resp = $"Hello{(!String.IsNullOrEmpty(userName) ? $", {userName}" : "")}!\nWelcome to AmGPGU_IT_bot";
                                        await botClient.SendTextMessageAsync(chat.Id, resp);
                                    }
                                }
                            }

                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}