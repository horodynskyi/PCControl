using System.Diagnostics;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Commands.PcCommands;

public class AnydeskCommand:ICommand
{
    private string Name => "/anydesk";
    private AnyDesk _anyDesk;
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(Name);
    }

    public async Task Execute(ITelegramBotClient client, Message message)
    {
        try
        {
            using (var reader = new StreamReader(@"AnyDeskPath.json"))
            {
                var str = await reader.ReadToEndAsync();
                _anyDesk = JsonConvert.DeserializeObject<AnyDesk>(str);
            }
            var proc = Process.Start(_anyDesk.Path);
            if (!proc.HasExited)
            {
                await client.SendTextMessageAsync(message.Chat.Id,"AnyDesk is running...");
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id,"AnyDesk is not running...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}

public class AnyDesk
{
    public string Path { get; set; }
}