using Telegram.Bot;

namespace TgBot;

public static class DependencyInjection
{
    public static void AddTelegramClient(this IServiceCollection services, BotOptions options)
    {
        var client = new TelegramBotClient(options.Token);
        services.AddTransient<ITelegramBotClient>(x => client);
    }
}