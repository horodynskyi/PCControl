using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Commands;

public class ShutdownCommand:ICommand
{
    private string Name => "/shutdown";
    private string _time = "60";
    
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(Name) && !message.Text.Contains("cancel");
    }
    public async Task Execute(ITelegramBotClient client,Message message)
    {
        var msgSplit = message.Text.Split();
        if (msgSplit.Length == 2)
            _time = msgSplit[1];
        await client.SendTextMessageAsync(
            message.Chat.Id,
            "Command has sent Shutdown");
        Process.Start("shutdown", $"/s /t {_time}");
    }
}