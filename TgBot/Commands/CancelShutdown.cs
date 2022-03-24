using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Commands;

public class CancelShutdown:ICommand
{
    private string Name => "/shutdowncancel";
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(Name);
    }

    public async Task Execute(ITelegramBotClient client, Message message)
    {
        await client.SendTextMessageAsync(
            message.Chat.Id,
            "Shutdown canceled");
        Process.Start("shutdown", "/a");
    }
}