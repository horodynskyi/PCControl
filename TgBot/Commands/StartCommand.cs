using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Commands.PCCommands;

namespace TgBot.Commands;

public class StartCommand : ICommand
{
    private string Name => "/start";
    private List<ICommand> _commands;
    private ICommand _command;

    public StartCommand()
    {
        _commands = new List<ICommand>
        {
            new PcCommand()
        };
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

    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        if (message.Text.Contains(Name))
            return true;
        foreach (var command in _commands)
        {
            if (command.Contains(message))
            {
                _command = command;
                return true;
            }
        }
        return false;
    }
}