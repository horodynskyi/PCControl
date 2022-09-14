using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Commands.PCCommands;

public class PcCommand:ICommand
{
    private string Name => "PC";
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(Name);
    }

    public async Task Execute(ITelegramBotClient client, Message message)
    {
        var keyButtons = new List<List<KeyboardButton>>()
        {
            new()
            {
                new KeyboardButton("/shutdown 1800")

            },
            new()
            {
                new KeyboardButton("/shutdown 2700")
            },
            new()
            {
                new KeyboardButton("/shutdown 3600")
            },
            new()
            {
                new KeyboardButton("/shutdowncancel")
            },
            new()
            {
                new KeyboardButton("number")
            },
        };
        var keyBoard = new ReplyKeyboardMarkup(keyButtons);
        keyBoard.ResizeKeyboard = true;
        await client.SendTextMessageAsync(message.Chat.Id, "Shutdown commands",
            parseMode: ParseMode.Html, disableWebPagePreview: false, disableNotification: false, replyToMessageId: 0,
            replyMarkup: keyBoard);
    }
}