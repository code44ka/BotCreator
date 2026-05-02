using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TGBot.Core;

namespace TGBot.Services;

public static class SendMessage
{
    private static YmlParser ActionParser = new YmlParser("buttons.yml");
    private static string[] keys = ActionParser.GetKeys();

    private static long id;
    private static int? messageId;
    private static string text;
    private static ReplyMarkup markup;
    
    public static async Task MessageSendHandler(ITelegramBotClient botClient, Update update)
    { 
        id = update.Message?.Chat.Id ?? 0;
        messageId = update.Message?.Id;
        text = update.Message?.Text ?? "";
        markup = KeyBoardButtons.Init();

        bool reply = bool.Parse(GetAction(text, "reply"));
       
        if (reply)
        {
            await MessageSendReply(botClient, update);
            
            return;
        }
        
        await MessageSend(botClient, update);
    }
    
    public static async Task MessageSend(ITelegramBotClient botClient, Update update)
    {
        await botClient.SendMessage(id, GetAction(text), replyMarkup: markup);
    }

    public static async Task MessageSendReply(ITelegramBotClient botClient, Update update)
    {
        await botClient.SendMessage(id, GetAction(text), replyMarkup: markup, replyParameters: messageId);
    }
    
    private static string GetAction(string key, string val = "action")
    {
        foreach (string k in keys)
        {
            if (ActionParser.Get($"{k}.text") == key || k == key) return ActionParser.Get($"{k}.{val}");
        }
        
        return "Я не знаю такой команды :(";
    }
}