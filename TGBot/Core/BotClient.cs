using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TGBot.Services;

namespace TGBot.Core;

public class BotClient
{
    private static TelegramBotClient botClient;

    public BotClient(string token) => StartBot(token);


    public static event Action<ITelegramBotClient, Update> OnMessage;
    
    private async void StartBot(string token)
    {
        try
        {
            botClient = new TelegramBotClient(token);
        }
        catch (Exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Не удалось запустить бота");
            Console.ResetColor();
            
            return;
        }
        
        botClient.StartReceiving(UpdateHandler, ErrorHandler);
        
        var me = await botClient.GetMe();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Бот {me.Username} успешно запущен!");
        Console.ResetColor();
    }

    private async Task UpdateHandler(ITelegramBotClient TGBotClient, Update update, CancellationToken arg3)
    {
        OnMessage?.Invoke(TGBotClient, update);
        
        await Task.CompletedTask;
    }

    private async Task ErrorHandler(ITelegramBotClient TGBotClient, Exception exception, HandleErrorSource arg3, CancellationToken arg4)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(exception.Message);
        Console.ResetColor();
        
        await Task.CompletedTask;
    }
}