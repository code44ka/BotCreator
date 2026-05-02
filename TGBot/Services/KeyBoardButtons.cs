using Telegram.Bot.Types.ReplyMarkups;
using TGBot.Core;

namespace TGBot.Services;

public static class KeyBoardButtons
{
    private static YmlParser ButtonsParser =  new YmlParser("buttons.yml");
    private static string[] keys;
    
    public static ReplyKeyboardMarkup Init()
    {
        keys = ButtonsParser.GetKeys();
        
        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup();

        int columns = GetButtonsColumns();
        List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
        
        for (int i = 0; i <= columns; i++)
        {
            rows.Add(AddButtons(i));
        }

        return new ReplyKeyboardMarkup(rows.ToArray())
        {
            ResizeKeyboard = true
        };
    }

    private static List<KeyboardButton> AddButtons(int column)
    {
        List<KeyboardButton> buttons = new List<KeyboardButton>();
        
        foreach (string key in keys)
        {
            if (Convert.ToInt32(ButtonsParser.Get($"{key}.column")) == column)
            {
                buttons.Add(new KeyboardButton(ButtonsParser.Get($"{key}.text")));
            }
        }
        
        return buttons;
    }
    
    private static int GetButtonsColumns()
    {
        List<int> columns = new List<int>();
        
        foreach (string key in keys)
        {
            columns.Add(Convert.ToInt32(ButtonsParser.Get($"{key}.column")));
        }
        
        return columns.Max();
    }
}