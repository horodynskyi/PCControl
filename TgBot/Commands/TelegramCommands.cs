using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Commands;

public class TelegramCommands:ICommand
{
    private readonly List<ICommand> _commands;
    private  ICommand _command;
    public TelegramCommands()
    {
        _commands = new List<ICommand>
        {
            new ShutdownCommand(),
            new CancelShutdown()
        };
       
    }
    public bool Contains(Message message)
    {
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

    public async Task Execute(ITelegramBotClient client, Message message)
    {
        await _command.Execute(client,message);
    }
}