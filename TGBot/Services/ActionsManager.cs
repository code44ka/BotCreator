using TGBot.Core;

namespace TGBot.Services;

public static class ActionsManager
{
    public static void AddActions()
    {
        BotClient.OnMessage += async (client, update) =>
        {
            await SendMessage.MessageSendHandler(client, update);
        };
    }
}