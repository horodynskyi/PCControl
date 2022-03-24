using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot;

public interface ICommand
{
    bool Contains(Message message);
    Task Execute(ITelegramBotClient client, Message message);
}